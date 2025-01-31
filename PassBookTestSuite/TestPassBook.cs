using Microsoft.Testing.Platform.Extensions.Messages;
using OSPassBook;
using OSPassBook.Structures;

namespace PassBookTestSuite
{
    [TestClass]
    public sealed class TestPassBook
    {
        [TestMethod]
        public void GeneratePass()
        {
            OSFullPass _testData = new OSFullPass();
            _testData.Header.organizationName = "OutSystems";
            _testData.Header.description = "NEO United VS Liberty NEO";
            _testData.Header.teamIdentifier = "YH3DK5DXQD";
            _testData.Header.serialNumber = "1234567890";


            _testData.type = OSPassTypes.EventTicket;
            _testData.transitTypes = OSTransitTypes.Generic;
            

            byte[] icon = File.ReadAllBytes("./Assets/Logo.png");
            _testData.icon.image = icon;
            _testData.icon.image2x = icon;
            _testData.icon.image3x = icon;

            byte[] background = File.ReadAllBytes("./Assets/background.png");
            _testData.background.image = background;
            _testData.background.image2x = background;
            _testData.background.image3x = background;

            byte[] logo = File.ReadAllBytes("./Assets/logo.png");
            _testData.logo.image = logo;
            _testData.logo.image2x = logo;
            _testData.logo.image3x = logo;

            byte[] thumbnail = File.ReadAllBytes("./Assets/Thumbnail.png");
            _testData.thumbnail.image = thumbnail;
            _testData.thumbnail.image2x = thumbnail;
            _testData.thumbnail.image3x = thumbnail;

            byte[] appleCert = File.ReadAllBytes("./Assets/AppleWWDRCAG4.cer");
            byte[] devCert = File.ReadAllBytes("./Assets/CertificatePassId.p12");
            _testData.Certificates.AppleWWDRCACertificate = appleCert;
            _testData.Certificates.PassbookCertificate = devCert;
            _testData.Certificates.PassbookCertificatePassword = "Password";

            _testData.primaryFields = new List<OSPassField>();
            _testData.primaryFields.Add(new OSPassField() { key= "event-name", value="NEO United vs Liberty NEO", label= "Match" });
            
            _testData.secondaryFields = new List<OSPassField>();
            _testData.secondaryFields.Add(new OSPassField() { key = "doors-open", value = DateTime.Now.ToString(), label = "Doors Open" });
            _testData.secondaryFields.Add(new OSPassField() { key = "seating", value = "2F 150", label = "Seating" });
            _testData.secondaryFields.Add(new OSPassField() { key = "door", value = "5", label = "Door" });

            _testData.auxiliaryFields = new List<OSPassField>();    
            _testData.auxiliaryFields.Add(new OSPassField() { key = "fan-id", label = "Name", value = "Carlos Perdigão" });

            _testData.barCode = "NEO UNITED VS NEO UNITED B CHAMPIONSHIP MATCH";

            _testData.Color = new OSPassColor() { LabelColor = "rgb(255,255,255)", ForegroundColor = "", BackgroundColor = "" };

            _testData.expireDate = DateTime.Now.AddDays(5);

            IOSPassGenerator generator = new OSPassGenerator();

            byte[] testResult = generator.generatePassFullInfo(_testData);

            File.WriteAllBytes("./testPass.pkpass",testResult);

        }
    }
}
