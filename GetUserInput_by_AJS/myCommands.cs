// (C) Copyright 2024 by
//
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

// This line is not mandatory, but improves loading performances
[assembly: CommandClass(typeof(GetUserInput_by_AJS.MyCommands))]

namespace GetUserInput_by_AJS
{
    public partial class MyCommands
    {
        [CommandMethod("GetPointWCS", CommandFlags.Modal)]
        public void GetPointWCS()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;

            if (doc != null)
            {
                Editor ed = doc.Editor;

                var pt = ed.GetPointWCS();
                if (EditorExtensions.GetResult == PromptStatus.OK)
                {
                    Circle cc = new Circle(pt, Autodesk.AutoCAD.Geometry.Vector3d.ZAxis, 10);
                    cc.ToDatabase();
                }
            }
            //Example by www.lisp.vn
        }

        [CommandMethod("Get2Point", CommandFlags.Modal)]
        public void Get2Point_WCS()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed;
            if (doc != null)
            {
                ed = doc.Editor;
                Line ln = ed.GetLine();
                if (ln != null)
                {
                    ln.ToDatabase();
                }
                return;
            }
            //Example by www.lisp.vn
        }
    }
}