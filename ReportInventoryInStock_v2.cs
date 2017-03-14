//1.0.1 14 Oct. 2016 LeestaPartNumber Added to the list

using NXOpen;
using NXOpen.Assemblies;
using NXOpen.UF;
using NXOpen.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ReadXMLFile
{
    [Serializable]
    public class InventoryList
    {
        public int ID { get; set; }
        public string PartNumber { get; set; }
        public string LeestaPartNumber{ get; set; }
        public string Qty { get; set; }
        public string Location { get; set; }
        public string VendorPartNumber { get; set; }
		
        public InventoryList()
        { }

        public InventoryList(string partNumber, int id, string qty, string leestaPartNumber, string location, string vendorPartNumber)
        {
            PartNumber = partNumber;
            ID = id;
            Qty = qty;
            LeestaPartNumber= leestaPartNumber;
            Location = location;
			VendorPartNumber = VendorPartNumber;
        }
    }

    public class ReportInventoryInStock
    {
        private static Session theSession = NXOpen.Session.GetSession();
        private static Part workPart = theSession.Parts.Work;
        private static Part displayPart = theSession.Parts.Display;
        private static UFSession ufs = UFSession.GetUFSession();

        public static void Echo(string output)
        {
            theSession.ListingWindow.Open();
            theSession.ListingWindow.WriteLine(output);
            theSession.LogFile.WriteLine(output);
        }

        public static List<InventoryList> Deserialization()
        {
            // передаем в конструктор тип класса
            XmlSerializer formatter = new XmlSerializer(typeof(List<InventoryList>));
            try
            {
                // десериализация
                using (FileStream fs = new FileStream(@"L:\Tooling Fixtures\StandardPartsInventoryExt.xml", FileMode.OpenOrCreate))
                {
                    List<InventoryList> il = (List<InventoryList>)formatter.Deserialize(fs);
                    return il;
                }
            }

            catch (NXOpen.NXException ex)
            {
                // ---- Enter your exception handling code here ----- 
                UI.GetUI().NXMessageBox.Show("Caught exception",
                         NXMessageBox.DialogType.Error, ex.Message);
                return null;
            }
        }

        public static void toDo()
        {
            try
            {
                Component root = theSession.Parts.Display.ComponentAssembly.RootComponent;

                if (root != null)
                {
                    List<InventoryList> il = Deserialization();
                    //Create component list, <PartName>, <Qty>
                    Dictionary<string, int> cl = new Dictionary<string, int>();
                    //Add to the dictionary copmonet values
                    foreach (var comp in root.GetChildren())
                    {
                        if (!(cl.ContainsKey(comp.Name)))
                        {
                            cl.Add(comp.Name, 0);
                        }
                        if (cl.ContainsKey(comp.Name))
                        {
                            cl[comp.Name] = cl[comp.Name] + 1;
                        }
                    }
                    
					int maxNameLen = 15;
					int c = 0;
					String formatedStr = "";
					String fileName = Path.GetFileName(workPart.FullPath);
										
					foreach (var child in cl)
                    {
                        //Echo(child.Key);
                        try
                        {
                            if (il.Exists(l => l.PartNumber.ToUpper() == child.Key.ToUpper()))
                            {
                                var match = il.Find(l => l.PartNumber.ToUpper() == child.Key.ToUpper());
								if (child.Key.Length > maxNameLen)
								{
									maxNameLen = child.Key.Length + 2;
								}
								c++;
                            }
                        }
                        catch (Exception ex)
                        {
                            Echo(ex.ToString());
                        }
						
                    }
					
					if (c>=1){
						
						
						formatedStr = "{0,-15} || {1,-" + maxNameLen + "} || {2,-12} || {3,-10} || {4,-10} || {5,10}\n";
											
						Echo("Standard components for " + fileName) ;
						
						Echo(String.Format(
						formatedStr,
							"Assembly Qty", 
							"Part Number", 
							"Qty In Stock", 
							"Location", 
							"Leesta P/N",
							//"Vendor",
							"Vendor P/N"
						)
							);
						foreach (var child in cl)
						{
							//Echo(child.Key);
							try
							{
								if (il.Exists(l => l.PartNumber.ToUpper().Replace(" ", "") == child.Key.ToUpper().Replace(" ", "")))
								{
									var match = il.Find(l => l.PartNumber.ToUpper().Replace(" ", "") == child.Key.ToUpper().Replace(" ", ""));
									Echo(
									String.Format(
									formatedStr,
									child.Value,
									child.Key,
									match.Qty,
									match.Location,
									match.LeestaPartNumber,
									match.VendorPartNumber
									)
									);
								}
							}
							catch (Exception ex)
							{
								Echo(ex.ToString());
							}

					
						}
						
						
						
						
					}
					else
					{
						
						Echo("There no standard components was found in " + fileName + "...");
					}
						
				}	


                else
                {
                    Echo("Part is not an assembly");
                }
				

            }

            catch (NXOpen.NXException ex)
            {
                // ---- Enter your exception handling code here ----- 
                UI.GetUI().NXMessageBox.Show("Caught exception",
                         NXMessageBox.DialogType.Error, ex.Message);
            }
        }

        public static void Main(string[] args)
        {
            try
            {
                if ((workPart != null))
                {
                    toDo();
                }
                else
                {
                    Echo("There is no open part!");
                }

            }
            catch (NXOpen.NXException ex)
            {
                // ---- Enter your exception handling code here ----- 
                UI.GetUI().NXMessageBox.Show("Caught exception",
                         NXMessageBox.DialogType.Error, ex.Message);
            }
        }

        public static int GetUnloadOption(string dummy) { return (int)NXOpen.Session.LibraryUnloadOption.Immediately; }
    }
}


