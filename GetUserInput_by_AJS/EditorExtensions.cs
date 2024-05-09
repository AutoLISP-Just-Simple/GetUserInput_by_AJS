using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using System.Diagnostics;

namespace GetUserInput_by_AJS
{
    public static class EditorExtensions
    {
        public static PromptStatus GetResult = PromptStatus.None;

        public static Point3d GetPointWCS(this Editor ed, string msg = "\nSpecify a point", bool allowNone = true)
        {
            var ppo = new PromptPointOptions(msg);
            ppo.AllowNone = allowNone;

            var psr = ed.GetPoint(ppo);
            GetResult = psr.Status;

            //Example by www.lisp.vn
            return psr.Status == PromptStatus.OK ? psr.Value.ToWCS() : Point3d.Origin;
        }

        public static ObjectId ToDatabase(this Entity e)
        {
            ObjectId retId = ObjectId.Null;
            var db = HostApplicationServices.WorkingDatabase;
            using (var tr = HostApplicationServices.WorkingDatabase.TransactionManager.StartTransaction())
            {
                var btr = tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;
                retId = btr.AppendEntity(e);
                tr.AddNewlyCreatedDBObject(e, true);
                tr.Commit();
            }
            //Example by www.lisp.vn
            return retId;
        }

        public static Line GetLine(this Editor ed)
        {
            var ppo = new PromptPointOptions("\nSpecify first point");
            ppo.AllowNone = true;
            var ppr = ed.GetPoint(ppo);
            if (ppr.Status != PromptStatus.OK) return null;

            var p_UCS = ppr.Value;

            var p1 = p_UCS.ToWCS();

            ppo.Message = "\nSpecify second point";
            ppo.BasePoint = p1.ToUCS();
            ppo.UseBasePoint = true;

            ppr = ed.GetPoint(ppo);
            if (ppr.Status != PromptStatus.OK) return null;

            var p2 = ppr.Value.ToWCS();

            //Example by www.lisp.vn
            return new Line(p1, p2);
        }

        public static Point3d ToWCS(this Point3d pt)
        {
            //Example by www.lisp.vn
            return pt.TransformBy(Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.CurrentUserCoordinateSystem);
        }

        public static Point3d ToUCS(this Point3d pt)
        {
            //Example by www.lisp.vn
            return pt.TransformBy(Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.CurrentUserCoordinateSystem.Inverse());
        }

        public static void Princ(string msg)
            => Debug.Print(msg);
    }
}