using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TapeAndReal_Carrier_Valid
{
    class comman
    {
        public static async Task SendMipcCommandAsync(string user, string Carrier, string State, string Real) {
            //MessageBox.Show(Carrier + "" + State + "" + Real);
            //Real = "AD000000000000004207";
            //State = "Valid";
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "http://tbwops01:1003/api/Values/IPC_talk"))
                {
                    StreamWriter sw = new StreamWriter(@"C:\TapeAndReal_Carrier_Valid\log.txt", true);//set the log path
                    request.Headers.TryAddWithoutValidation("accept", "text/plain");
                    String Message = "<CTSCarrierMapUpdate><Container><Id>" + Real + "</Id><Type>TAPE AND REEL</Type><MapState>Valid</MapState><LastUpdateBy>KTF160-0001</LastUpdateBy><ChildContainers><ChildContainer><Id>" + Real + "</Id><Type>REELTAPE</Type><X>1</X><Y/><Z/><PlacementId/></ChildContainer></ChildContainers></Container></CTSCarrierMapUpdate>\"\n}";
                    Dictionary<string, string> jsonValues = new Dictionary<string, string>();
                    jsonValues.Add("Destination", "/TAICHUNG_BE/MTI/MFG/MESSRV/PROD/SERVER/MESSRV");
                    jsonValues.Add("Message", "<CTSCarrierMapUpdate><Container><Id>"+ Real + "</Id><Type>TAPE AND REEL</Type><MapState>" + State + "</MapState><LastUpdateBy>KTF160-0001</LastUpdateBy><ChildContainers><ChildContainer><Id>" + Real + "</Id><Type>REELTAPE</Type><X>1</X><Y/><Z/><PlacementId/></ChildContainer></ChildContainers></Container></CTSCarrierMapUpdate>");
                    jsonValues.Add("Note", "Modified by "+ user);
                    
                    request.Content = new StringContent(JsonConvert.SerializeObject(jsonValues), UnicodeEncoding.UTF8, "application/json");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json-patch+json");
                    HttpResponseMessage response = await httpClient.SendAsync(request);
                    sw.WriteLine(DateTime.Now.ToString("MM-dd-yyyy-hh:mm:ss") +" "+response);
                    //sw.WriteLine(response);
                    var jsonString = await response.Content.ReadAsStringAsync();
                    //dynamic _result = JToken.Parse(jsonString);

                    if (jsonString.Contains("Exception"))
                    {
                        MessageBox.Show("Fail!");
                        sw.WriteLine(DateTime.Now.ToString("MM-dd-yyyy-hh:mm:ss") + " " + Message); // MIPC command
                        sw.WriteLine("Fail!");
                        sw.WriteLine(jsonString);
                    }
                    else {
                        MessageBox.Show("Successfully");
                        sw.WriteLine(DateTime.Now.ToString("MM-dd-yyyy-hh:mm:ss") + " " + jsonString);//successfully message
                        sw.WriteLine("Successfully!");
                    }
                    sw.Close();
                }
            }
        }


        public static async Task SendMipcCommand_SubstrateLotAssociation(string user, string Real, string Lot)
        {
            //MessageBox.Show(Carrier + "" + State + "" + Real);
            //Real = "AD000000000000004207";
            //State = "Valid";
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "http://tbwops01:1003/api/Values/IPC_talk"))
                {
                    StreamWriter sw = new StreamWriter(@"C:\TapeAndReal_Carrier_Valid\log.txt", true);//set the log path
                    request.Headers.TryAddWithoutValidation("accept", "text/plain");
                    String Message = "<SubstrateLotAssociation><Input><Facility>Assembly</Facility><SubstrateList><Substrate><SubstrateId>" + Real + "</SubstrateId><SubstrateType>REELTAPE</SubstrateType><LotList><Lot><LotId>" + Lot + "</LotId><LotType>LOT</LotType><Action>ADD</Action></Lot></LotList></Substrate></SubstrateList></Input></SubstrateLotAssociation>";
                    Dictionary<string, string> jsonValues = new Dictionary<string, string>();
                    jsonValues.Add("Destination", "/TAICHUNG_BE/MTI/MFG/MESSRV/PROD/SERVER/MESSRV");
                    jsonValues.Add("Message", "<SubstrateLotAssociation><Input><Facility>Assembly</Facility><SubstrateList><Substrate><SubstrateId>" + Real + "</SubstrateId><SubstrateType>REELTAPE</SubstrateType><LotList><Lot><LotId>" + Lot + "</LotId><LotType>LOT</LotType><Action>ADD</Action></Lot></LotList></Substrate></SubstrateList></Input></SubstrateLotAssociation>");
                    jsonValues.Add("Note", "Modified by " + user);

                    request.Content = new StringContent(JsonConvert.SerializeObject(jsonValues), UnicodeEncoding.UTF8, "application/json");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json-patch+json");
                    HttpResponseMessage response = await httpClient.SendAsync(request);
                    sw.WriteLine(DateTime.Now.ToString("MM-dd-yyyy-hh:mm:ss") + " " + response);
                    //sw.WriteLine(response);
                    var jsonString = await response.Content.ReadAsStringAsync();
                    //dynamic _result = JToken.Parse(jsonString);

                    if (jsonString.Contains("Exception"))
                    {
                        MessageBox.Show("Fail!");
                        sw.WriteLine(DateTime.Now.ToString("MM-dd-yyyy-hh:mm:ss") + " " + Message); // MIPC command
                        sw.WriteLine("Fail!");
                        sw.WriteLine(jsonString);
                    }
                    else
                    {
                        MessageBox.Show("Successfully");
                        sw.WriteLine(DateTime.Now.ToString("MM-dd-yyyy-hh:mm:ss") + " " + jsonString);//successfully message
                        sw.WriteLine("Successfully!");
                    }
                    sw.Close();
                }
            }
        }



        public static async Task SendEmailAsync(string user,string Carrier, string State, string Real) {
            Dictionary<string, string> jsonValues = new Dictionary<string, string>();
            jsonValues.Add("Title", "TapeAndReal_Carrier_Valid Tool execution");
            jsonValues.Add("Content", DateTime.Now.ToString("MM-dd-yyyy-hh:mm:ss") +" "+ user +" "+ "change"+" "+ Real + " state to "+State+".");
            jsonValues.Add("Send_From", "TBMES@micron.com");
            jsonValues.Add("Send_To", "wardcheng@micron.com,kenjichou@micron.com,ALL_KENJICHOU_DIRECT_EMPLOYEE_REPORTS@micron.com,lichiasin@micron.com");
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "http://tbwops01:1003/api/Values/SendEmail"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "*/*");
                    request.Content = new StringContent(JsonConvert.SerializeObject(jsonValues), UnicodeEncoding.UTF8, "application/json");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json-patch+json");

                    var response = await httpClient.SendAsync(request);
                    MessageBox.Show("Send Mail Successfully.");
                }
            }
        }

        public static async Task SendEmailAsync_SubstrateLotAssociation(string user,  string Real, string Lot)
        {
            Dictionary<string, string> jsonValues = new Dictionary<string, string>();
            jsonValues.Add("Title", "TapeAndReal_Carrier_Valid Tool execution");
            jsonValues.Add("Content", DateTime.Now.ToString("MM-dd-yyyy-hh:mm:ss") + " " + user + " " + "Associate" + " " + Real + " with " + Lot + "Successfully!");
            jsonValues.Add("Send_From", "TBMES@micron.com");
            jsonValues.Add("Send_To", "wardcheng@micron.com,kenjichou@micron.com,ALL_KENJICHOU_DIRECT_EMPLOYEE_REPORTS@micron.com,lichiasin@micron.com");
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "http://tbwops01:1003/api/Values/SendEmail"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "*/*");
                    request.Content = new StringContent(JsonConvert.SerializeObject(jsonValues), UnicodeEncoding.UTF8, "application/json");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json-patch+json");

                    var response = await httpClient.SendAsync(request);
                    MessageBox.Show("Send Mail Successfully.");
                }
            }
        }
    }
}
