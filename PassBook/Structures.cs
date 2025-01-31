using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OutSystems.ExternalLibraries.SDK;

namespace OSPassBook.Structures
{
    [OSStructure]
    public struct OSPassHeader
    {
        [OSStructureField]
        public string description;
        [OSStructureField]
        public string organizationName;
        [OSStructureField]
        public string passTypeIdentifier;
        [OSStructureField]
        public string serialNumber;
        [OSStructureField]
        public string teamIdentifier;

        /*public Pass(string _description, string _orgName, string _passTypeId, string _serialNumber, string _teamIdentifier)
        {
            description = _description;
            organizationName = _orgName;
            passTypeIdentifier = _passTypeId;
            serialNumber = _serialNumber;
            teamIdentifier = _teamIdentifier;
        }*/
    }

    [OSStructure]
    public struct OSPassColor
    {
        [OSStructureField]
        public string BackgroundColor;
        [OSStructureField]
        public string LabelColor;
        [OSStructureField]
        public string ForegroundColor;
    }

    [OSStructure]
    public struct OSPassCertificates
    {
        [OSStructureField]
        public byte[] AppleWWDRCACertificate;

        [OSStructureField]
        public byte[] PassbookCertificate;

        [OSStructureField]
        public string PassbookCertificatePassword;

    }

    [OSStructure]
    public struct OSMinimalPass
    {
        [OSStructureField]
        public OSPassHeader Header;
        
        [OSStructureField]
        public OSPassCertificates Certificates;
        
        [OSStructureField]
        public OSPassImage icon;

        [OSStructureField]
        public OSPassTypes _type;

        [OSStructureField]
        public OSTransitTypes _tt;
    }

    [OSStructure]
    public struct OSFullPass
    {
        [OSStructureField]
        public OSPassHeader Header;
        
        [OSStructureField]
        public OSPassColor Color;
        
        [OSStructureField]
        public OSPassCertificates Certificates;

        [OSStructureField]
        public OSPassImage icon;

        [OSStructureField]
        public OSPassTypes type;

        [OSStructureField]
        public OSTransitTypes transitTypes;

        [OSStructureField]
        public List<OSPassField> primaryFields;

        [OSStructureField]
        public List<OSPassField> secondaryFields;

        [OSStructureField] 
        public List<OSPassField> auxiliaryFields;

        [OSStructureField]
        public string barCode;

        [OSStructureField]
        public OSPassImage background;

        [OSStructureField]
        public OSPassImage thumbnail;

        [OSStructureField]
        public OSPassImage logo;

        [OSStructureField]
        public DateTime expireDate;


    }

    public enum OSPassTypes{
        Generic = 0,
        BoardingPass = 1,
        Coupon = 2,
        EventTicket = 3,
        StoreCard = 4
    }

    public enum OSTransitTypes
    {
        Air = 0,
        Boat = 1,
        Bus = 2,
        Generic = 3,
        Train = 4
    }

    [OSStructure]
    public struct OSPassImage
    {
        [OSStructureField]
        public byte[] image;

        [OSStructureField]
        public byte[] image2x;

        [OSStructureField]
        public byte[] image3x;
    }

    [OSStructure]
    public struct OSPassField
    {
        [OSStructureField]
        public string key;
        [OSStructureField]
        public string label;
        [OSStructureField]
        public string value;
    }

}