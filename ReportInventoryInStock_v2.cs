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

        // public InventoryList(string partNumber, int id, string qty, string leestaPartNumber, 
		// string location, string vendorPartNumber)
        // {
            // PartNumber = partNumber;
            // ID = id;
            // Qty = qty;
            // LeestaPartNumber= leestaPartNumber;
            // Location = location;
			// VendorPartNumber = VendorPartNumber;
        // }
    }
    public class ReportInventoryInStock
    {
		public const String xmlLocation = @"L:\Tooling Fixtures\StandardPartsInventory.xml";
		//public const String xmlLocation = @"L:\Tooling Fixtures\StandardPartsInventoryExt.xml";
		
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
                using (FileStream fs = 
				new FileStream(xmlLocation, FileMode.OpenOrCreate))
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
					int count = 0;
					
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
					int maxNameLen = 46; //15
					
					String formatedStr = "";
					String fileName = Path.GetFileName(workPart.FullPath);
					foreach (var child in cl)
                    {
                        //Echo(child.Key);
                        
                    }
					if (true){
						formatedStr = "{0,-15} || {1,-" + maxNameLen + "} || {2,-12} || {3,-10} || {4,-10} || {5,10}\n";
						Echo("Standard components for " + fileName) ;						
						Echo(String.Format
								(
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
						Echo(new String('=', (15+maxNameLen+12+10+10+10+20)));
						foreach (var child in cl)
						{
							//Echo(child.Key);
							try
							{
								//var match = null;
								
								String childName = child.Key.ToUpper().Replace(" ", "");							
								
								
								foreach (var listItem in il)	
								{
								    
									if (childName == listItem.PartNumber.ToUpper().Replace(" ", "")
										| childName == listItem.VendorPartNumber.ToUpper().Replace(" ", "")
										| childName == listItem.LeestaPartNumber.ToUpper().Replace(" ", "")
										
									)
									{
										Echo(
											String.Format(
												formatedStr,
												child.Value,
												child.Key,
												listItem.Qty,
												listItem.Location,
												listItem.LeestaPartNumber,
												listItem.VendorPartNumber
												)
											);
										count++;
									}
									
								}
							}
							catch (Exception ex)
							{
								//Echo(ex.ToString());
							}
						}
					}
					if (count==0)
						{
							Echo("There are no standard components was found in " + fileName + "...");
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





