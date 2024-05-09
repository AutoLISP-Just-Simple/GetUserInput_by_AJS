Begin with GetUserInput functions in AutoCAD dotNet (c#)
Using:

        [CommandMethod("GetPointWCS", CommandFlags.Modal)]
        public void GetPointWCS()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
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
Source: https://www.lisp.vn/2024/05/lay-toa-do-tu-nguoi-dung-autocad-dotnet.html
