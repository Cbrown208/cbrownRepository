using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLReader
{
    public class ReadXMLFunction
    {

        public static StringBuilder SearchXML(string xml, string value)
        {
            var dti = "dti";
            var residualIncome = "residualIncome";
            var availableCredit = "availableCredit";
            var estHouseholdIncome = "estHouseholdIncome";
            var estHouseholdSize = "estHouseholdSize";

            var scoreModel = "scoreModel";
            var code = "code";
            var results = "results";
            var derogatoryAlert = "derogatoryAlert";
            var fileInquiriesImpactedScore = "fileInquiriesImpactedScore";
            var scoreCard = "scoreCard";

            StringBuilder output2 = new StringBuilder();
            using (XmlReader reader = XmlReader.Create(new StringReader(xml)))
            {
                //reader.ReadToFollowing("book");
                //reader.MoveToFirstAttribute();
                //string genre = reader.Value;
                //output2.AppendLine("The genre value: " + genre);

                reader.ReadToDescendant("calculations");

                reader.ReadToFollowing(dti);
                var temp = reader.ReadElementContentAsString();
                output2.AppendLine(dti + ": " + temp);
                
                reader.ReadToFollowing(residualIncome);
                var temp2 = reader.ReadElementContentAsString();
                output2.AppendLine(residualIncome + ": " + temp2);

                reader.ReadToFollowing(availableCredit);
                var temp3 = reader.ReadElementContentAsString();
                output2.AppendLine(availableCredit + ": " + temp3);
                
                reader.ReadToFollowing(estHouseholdIncome);
                var temp4 = reader.ReadElementContentAsString();
                output2.AppendLine(estHouseholdIncome + ": " + temp4);
                
                reader.ReadToFollowing(estHouseholdSize);
                var temp5 = reader.ReadElementContentAsString();
                output2.AppendLine(estHouseholdSize + ": " + temp5);
                //output2.AppendLine( + value + ": " + reader.ReadElementContentAsString());

                //reader.ReadToFollowing(value);
                //reader.ReadToFollowing(value);
                //output2.AppendLine(+value+": " + temp);

            }
            using (XmlReader reader = XmlReader.Create(new StringReader(xml)))
            {
                //reader.ReadToFollowing("book");
                //reader.MoveToFirstAttribute();
                //string genre = reader.Value;
                //output2.AppendLine("The genre value: " + genre);
                output2.AppendLine(" ");
                reader.ReadToDescendant(scoreModel);

                reader.ReadToFollowing(code);
                var temp = reader.ReadElementContentAsString();
                output2.AppendLine(code + ": " + temp);

                reader.ReadToFollowing(results);
                var temp2 = reader.ReadElementContentAsString();
                output2.AppendLine(results + ": " + temp2);

                reader.ReadToFollowing(derogatoryAlert);
                var temp3 = reader.ReadElementContentAsString();
                output2.AppendLine(derogatoryAlert + ": " + temp3);

                reader.ReadToFollowing(fileInquiriesImpactedScore);
                var temp4 = reader.ReadElementContentAsString();
                output2.AppendLine(fileInquiriesImpactedScore + ": " + temp4);

                reader.ReadToFollowing(scoreCard);
                var temp5 = reader.ReadElementContentAsString();
                output2.AppendLine(scoreCard + ": " + temp5);


                output2.AppendLine(" ");

                reader.ReadToFollowing(code);
                var temp6 = reader.ReadElementContentAsString();
                output2.AppendLine(code + ": " + temp6);
                
                reader.ReadToFollowing(results);
                var temp7 = reader.ReadElementContentAsString();
                output2.AppendLine(results + ": " + temp7);

                reader.ReadToFollowing(derogatoryAlert);
                var temp8 = reader.ReadElementContentAsString();
                output2.AppendLine(derogatoryAlert + ": " + temp8);

                reader.ReadToFollowing(fileInquiriesImpactedScore);
                var temp9 = reader.ReadElementContentAsString();
                output2.AppendLine(fileInquiriesImpactedScore + ": " + temp9);

                reader.ReadToFollowing(scoreCard);
                var temp10 = reader.ReadElementContentAsString();
                output2.AppendLine(scoreCard + ": " + temp10);

                // Third Score
                output2.AppendLine(" ");

                reader.ReadToFollowing(code);
                var temp11 = reader.ReadElementContentAsString();
                output2.AppendLine(code + ": " + temp11);

                reader.ReadToFollowing(results);
                var temp12 = reader.ReadElementContentAsString();
                output2.AppendLine(results + ": " + temp12);

                reader.ReadToFollowing(derogatoryAlert);
                var temp13 = reader.ReadElementContentAsString();
                output2.AppendLine(derogatoryAlert + ": " + temp13);

                reader.ReadToFollowing(fileInquiriesImpactedScore);
                var temp14 = reader.ReadElementContentAsString();
                output2.AppendLine(fileInquiriesImpactedScore + ": " + temp14);

                reader.ReadToFollowing(scoreCard);
                var temp15 = reader.ReadElementContentAsString();
                output2.AppendLine(scoreCard + ": " + temp15);

            }
            return output2;
        }
    }
}
