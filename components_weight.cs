using System;
 
namespace Gust
{

    public class components_weight
    {
        public static void Main(String[] args)
        {
            //with clues from nxjournaling.com

            NXOpen.Session session = NXOpen.Session.GetSession();
            NXOpen.Part part = session.Parts.Display;
            NXOpen.ListingWindow infoWindow = session.ListingWindow;
 
            if (!infoWindow.IsOpen) {
                infoWindow.Open();
				infoWindow.WriteLine("Started");
            }

            if (part.PartUnits != NXOpen.BasePart.Units.Inches) {
                //dont do anything if the units are inches.
				infoWindow.WriteLine("Mweasure units are not Inches");
                return;
            }
 
 
            NXOpen.Unit[] unitArray = new NXOpen.Unit[5];
            unitArray[0] = part.UnitCollection.GetBase("Area");
            unitArray[1] = part.UnitCollection.GetBase("Volume");
            unitArray[2] = part.UnitCollection.GetBase("Mass");
            unitArray[3] = part.UnitCollection.GetBase("Length");
            unitArray[4] = part.UnitCollection.GetBase("Force");//unit of weight
 
            var mb = part.MeasureManager.NewMassProperties(unitArray, 0.01, part.Bodies.ToArray());
            mb.InformationUnit = NXOpen.MeasureBodies.AnalysisUnit.PoundInch;
 
 
            //infoWindow.WriteLine("Area: " + mb.Area);
            //infoWindow.WriteLine("Volume: " + mb.Volume);
            infoWindow.WriteLine("Mass: " + mb.Mass + " pounds (" + mb.Mass * .453592 + " kg)");
            //infoWindow.WriteLine("Weight: " + mb.Weight);
            //infoWindow.WriteLine("Radius of Gyration: " + mb.RadiusOfGyration);
 
            //get advanced mass properties
            /*infoWindow.WriteLine("Measure types in part");
            foreach (var elem in part.UnitCollection.GetMeasures()) {
                infoWindow.WriteLine(elem);
            }*/
 
        }
 
        /// <summary>
        /// Gets the unload option. Function automatically called by NX
        /// </summary>
        /// <returns>The unload option.</returns>
        public static int GetUnloadOption()
        {
            return (int)NXOpen.Session.LibraryUnloadOption.Immediately;
        }
    }
}
