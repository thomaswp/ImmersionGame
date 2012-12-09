using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Immersion;
using System.Drawing;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace LevelEditor
{
    public class UIHandler
    {

        //actions from the list box
        public enum Actions
        {
            Move, Select, Platform, Segue
        }

        //segues from the listbox
        public enum Segues
        {
            Linear, Curved, Wait, Jump
        }

        public Actions CurrentActionType { get; set; }
        public Segues CurrentSegueType { get; set; }
        public int CurrentPorpertiesIndex { get; set; }

        private int Degree { get { return editorState.Degree; } }

        //contains all the information about the editor's current state
        private EditorState editorState;

        //if the user is draggin the mouse, keeps track of where the drag started
        private Point startDragMouse, startDragMap;
        private bool shiftDrag;
        private bool draggingMap;

        //platform being dragged
        private PlatformData draggingPlatform;
        //segue being dragged
        private PlatformSegue draggingSegue;
        //if the user is dragging something, where did they grab it
        private Vector2 draggingItemOffset;

        private PlatformDialog platformDialog = new PlatformDialog();

        //selected/highlighted platform
        private PlatformData SelectedPlatform
        {
            get { return editorState.SelectedPlatform; }
            set { editorState.SelectedPlatform = value; }
        }
        //selected/highlighted segue
        private PlatformSegue SelectedSegue
        {
            get { return editorState.SelectedSegue; }
            set { editorState.SelectedSegue = value; }
        }
        private List<PlatformData> SelectedPlatforms { get { return editorState.SelectedPlatforms;} }
        private List<PlatformSegue> SelectedSegues { get { return editorState.SelectedSegues; } }
        
        public UIHandler(EditorState editorState)
        {
            this.editorState = editorState;
        }

        public void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (SelectedPlatform != null)
                {
                    editorState.Map.Platforms.Remove(SelectedPlatform);
                    List<WordCloudData> toRemove = new List<WordCloudData>();
                    foreach (WordCloudData wordCloud in editorState.Map.WordClouds)
                    {
                        if (wordCloud.PathedObject == SelectedPlatform)
                        {
                            toRemove.Add(wordCloud);
                        }
                    }
                    foreach (WordCloudData wordCloud in toRemove)
                    {
                        editorState.Map.WordClouds.Remove(wordCloud);
                    }
                    SelectedPlatform = null;
                }
                if (SelectedSegue != null)
                {
                    foreach (PlatformData platform in editorState.Map.Platforms)
                    {
                        if (platform.segues.Contains(SelectedSegue))
                        {
                            platform.segues.Remove(SelectedSegue);
                        }
                    }
                }
            }
        }

        public void OnKeyPress(KeyPressEventArgs e)
        {
        }

        public void OnDoubleClick(EventArgs e)
        {
        }

        //Ugly method that handles all mouse down events. Needs to be cleaned
        public void MouseDown(MouseEventArgs e)
        {
            Vector2 pos = editorState.MousePosOnMap(e.Location);
            MapData map = editorState.Map;
            shiftDrag = false;

            if (e.Button == MouseButtons.Right)
            {
                if (SelectedPlatform != null || SelectedSegue != null)
                {
                    PlatformData addTo = SelectedPlatform;

                    if (addTo == null)
                    {
                        foreach (PlatformData platform in map.Platforms)
                        {
                            if (platform.StartSegue == SelectedSegue || platform.segues.Contains(SelectedSegue))
                            {
                                addTo = platform;
                                break;
                            }
                        }
                    }

                    //add a new segue and start dragging it
                    if (addTo == null) return;

                    PlatformSegue segue = null;
                    if (CurrentSegueType == Segues.Linear)
                    {
                        segue = new PlatformSegueLinear(pos);
                    }
                    else if (CurrentSegueType == Segues.Curved)
                    {
                        segue = new PlatformSegueCurved(pos);
                    }
                    else if (CurrentSegueType == Segues.Wait)
                    {
                        segue = new PlatformSegueJump(pos);
                    }
                    else if (CurrentSegueType == Segues.Jump)
                    {
                        segue = new PlatformSegueJump(pos);
                    }

                    if (segue == null) return;

                    int index = addTo.segues.Count;
                    if (SelectedSegue != null)
                    {
                        index = addTo.segues.IndexOf(SelectedSegue) + 1;
                    }

                    addTo.segues.Insert(index, segue);
                    draggingSegue = segue;
                    draggingItemOffset = new Vector2();
                }
                else
                {
                    draggingPlatform = new PlatformData(pos, Degree);
                    draggingItemOffset = new Vector2();
                    map.Platforms.Add(draggingPlatform);
                }
            }
            else
            {

                if (Control.ModifierKeys != Keys.Control)
                {
                    //set selections to null
                    SelectedPlatform = null;
                    SelectedSegue = null;
                    SelectedPlatforms.Clear();
                    SelectedSegues.Clear();
                }
                draggingPlatform = null;
                draggingSegue = null;

                if (SelectedPlatform == null)
                {
                    foreach (PlatformData platform in map.Platforms)
                    {
                        //check the start "segue" which is really just
                        //the start position of the platform
                        //but you can move that, so it's selectable
                        if (segueContains(platform.StartSegue, pos))
                        {
                            SelectedSegue = platform.StartSegue;
                            draggingSegue = SelectedSegue;
                            break;
                        }
                        //and check all the rest
                        foreach (PlatformSegue segue in platform.segues)
                        {
                            if (segueContains(segue, pos))
                            {
                                SelectedSegue = segue;
                                draggingSegue = SelectedSegue;
                                break;
                            }
                        }
                    }
                }

                //drag the selected segue
                if (draggingSegue != null)
                {
                    draggingItemOffset = SelectedSegue.Destination - pos;
                }
                if (SelectedSegue != null && !SelectedSegues.Contains(SelectedSegue))
                {
                    SelectedSegues.Add(SelectedSegue);
                }

                if (SelectedSegue == null)
                {
                    //look for a platform first
                    foreach (PlatformData platform in map.Platforms)
                    {
                        if (platform.contains(pos, Degree))
                        {
                            SelectedPlatform = platform;
                            draggingPlatform = platform;
                        }
                    }
                    if (SelectedPlatform != null)
                    {
                        SelectedPlatforms.Add(SelectedPlatform);
                        shiftDrag = Control.ModifierKeys == Keys.Shift;
                    }
                }

                //drag the selected platform
                draggingPlatform = SelectedPlatform;
                if (draggingPlatform != null)
                {
                    draggingItemOffset = SelectedPlatform.GetPosition(Degree) - pos;
                }

                //if we're not selecting anything, just drag the map
                if (SelectedPlatform == null && SelectedSegue == null)
                {
                    startMapDrag(e);
                }


                if (SelectedPlatform != null && e.Clicks == 2)
                {
                    draggingPlatform = null;
                    platformDialog.EditorState = editorState;
                    platformDialog.ShowDialog();
                }
            }
        }

        public void MouseUp(MouseEventArgs e)
        {
            //just clear dragging variables
            draggingMap = false;
            draggingPlatform = null;
            draggingSegue = null;
        }

        public void MouseMove(MouseEventArgs e)
        {
            Vector2 pos = mousePosOnMap(e.Location);
            if (draggingMap)
            {
                //pan the map
                editorState.MapOffset.X = startDragMap.X - (e.X - startDragMouse.X);
                editorState.MapOffset.Y = startDragMap.Y - (e.Y - startDragMouse.Y);
            }
            if (draggingPlatform != null)
            {
                if (draggingPlatform.segues.Count == 0)
                {
                    //if there are no segue, just move the platform
                    draggingPlatform.StartPos = pos + draggingItemOffset;
                }
                else
                {
                    //if there are segues, offset the platform's start degree
                    if (shiftDrag)
                    {
                        Vector2 offset = pos + draggingItemOffset - draggingPlatform.GetPosition(Degree);
                        draggingPlatform.StartPos += offset;
                        foreach (PlatformSegue segue in draggingPlatform.segues)
                        {
                            segue.Destination += offset;
                        }
                    }
                    else
                    {
                        updatePlatformOffset(SelectedPlatform, Degree, mousePosOnMap(e.Location));
                    }
                    //updatePlatformWeights(Degree, mousePosOnMap(e.Location));
                }
            }
            if (draggingSegue != null)
            {
                //move the dragging segue
                draggingSegue.Destination = pos + draggingItemOffset;
                foreach (PlatformData platform in editorState.Map.Platforms)
                {
                    if (platform.segues.Contains(draggingSegue) || platform.StartSegue == draggingSegue)
                    {
                        if (platform.StartSegue != draggingSegue && (platform.StartSegue.Destination - draggingSegue.Destination).Length() < 5)
                        {
                            draggingSegue.Destination = platform.StartSegue.Destination;
                        }
                        foreach (PlatformSegue segue in platform.segues)
                        {
                            if (segue != draggingSegue && (segue.Destination - draggingSegue.Destination).Length() < 5)
                            {
                                draggingSegue.Destination = segue.Destination;
                            }
                        }
                    }
                }
            }
        }

        public void MouseWheel(MouseEventArgs e)
        {
            if (SelectedSegue != null)
            {
                //experimental interface for modifying segue properties...
                SelectedSegue.ChangeProperty(CurrentPorpertiesIndex, e.Delta);
            }
        }

        private void startMapDrag(MouseEventArgs e)
        {
            draggingMap = true;
            startDragMouse = new Point(e.X, e.Y);
            startDragMap = editorState.MapOffset;
        }

        private void updatePlatformOffset(PlatformData platform, int degree, Vector2 pos)
        {
            //look for the closest position in the platform's path
            //to where the mouse is, then offset it so it's there now
            float desiredDegree = 0;
            float minDis = float.MaxValue;
            for (float deg = 0; deg < 360; deg += 0.1f)
            {
                float dis = (platform.GetPosition(deg) - pos).Length();
                if (dis < minDis)
                {
                    minDis = dis;
                    desiredDegree = deg;
                }
            }

            platform.DegreeOffset += desiredDegree - degree;
            platform.DegreeOffset = (platform.DegreeOffset + 360) % 360;
        }

        //experimental method to change segue "weights" to put the platform
        //where it's being dragged
        private void updatePlatformWeights(int degree, Vector2 pos)
        {
            if (degree == 0) return;
            if (SelectedPlatform.segues.Count < 2) return;

            int segueIndex = SelectedPlatform.GetCurrentSegueIndex(degree);
            if (segueIndex < 0) return;
            PlatformSegue segue = SelectedPlatform.segues[segueIndex];
            Vector2 start = segueIndex == 0 ? SelectedPlatform.StartPos :
                SelectedPlatform.segues[segueIndex - 1].Destination;

            float desiredPerc = 0;
            float minDis = float.MaxValue;
            for (float perc = 0; perc < 1; perc += 0.01f)
            {
                float dis = (segue.GetPosition(start, perc) - pos).Length();
                if (dis < minDis)
                {
                    minDis = dis;
                    desiredPerc = perc;
                }
            }

            float degreePerc = degree / 360f;

            if (degreePerc == desiredPerc) return;

            float weightBefore = 0, weightAfter = 0;
            for (int i = 0; i < SelectedPlatform.segues.Count; i++)
            {
                if (i < segueIndex)
                {
                    weightBefore += SelectedPlatform.segues[i].SegWeight;
                }
                if (i > segueIndex)
                {
                    weightAfter += SelectedPlatform.segues[i].SegWeight;
                }
            }

            float weight = degreePerc * (weightBefore + weightAfter) - weightBefore;
            weight /= desiredPerc - degreePerc;

            if (weight <= 0 || float.IsNaN(weight)) return;

            Console.WriteLine(weight);

            segue.SegWeight = weight;
        }

        private Vector2 mousePosOnMap(Point pos)
        {
            return editorState.MousePosOnMap(pos);
        }

        private Point mapPointOnCanvas(Vector2 pos)
        {
            return editorState.MapPointOnCanvas(pos);
        }

        private bool segueContains(PlatformSegue segue, Vector2 pos)
        {
            return (segue.Destination - pos).Length() < MapRenderer.SEGUE_DRAW_RADIUS;
        }
    }
}
