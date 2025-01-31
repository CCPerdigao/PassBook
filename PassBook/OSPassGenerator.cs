using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Passbook.Generator;
using Passbook.Generator.Fields;
using OSPassBook.Structures;
using OutSystems.ExternalLibraries.SDK;

namespace OSPassBook
{
    
    public class OSPassGenerator : IOSPassGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_genericInfo">Header information to be added to the PassBook. </param>
        /// <param name="_certificates">Certificates to sign the passbook</param>
        /// <returns></returns>
        /*[OSAction]
        public byte[] generatePassMinInfo(OSMinimalPass _pass)
        {
            PassGenerator generator = new PassGenerator();
            PassGeneratorRequest passRequest = generatePass(_pass.Header, _pass.Certificates);
            passRequest = addIcons(passRequest, _pass.icon);
            passRequest = setPassStyle(passRequest,_pass._type,_pass._tt); 
            return generator.Generate(passRequest);
        }*/

        [OSAction]
        public byte[] generatePassFullInfo(OSFullPass _pass)
        {
            PassGenerator generator = new PassGenerator();
            PassGeneratorRequest passRequest = generatePass(_pass.Header,_pass.Certificates);
            passRequest = addIcons(passRequest, _pass.icon);
            passRequest = setPassStyle(passRequest, _pass.type, _pass.transitTypes);
            passRequest = addPrimaryFields(passRequest, _pass.primaryFields);
            passRequest = addSecondaryFields(passRequest, _pass.secondaryFields);   
            passRequest = addAuxiliaryFields(passRequest, _pass.auxiliaryFields);
            passRequest = addBarCode(passRequest, _pass.barCode);
            passRequest = setRequestColor(passRequest, _pass.Color);
            passRequest = addBackground(passRequest, _pass.background);
            passRequest = addLogo(passRequest, _pass.logo);
            passRequest = addThumbnail(passRequest, _pass.thumbnail);
            passRequest = addExpireDates(passRequest, _pass);

            return generator.Generate(passRequest);
        }

        /*[OSAction]
        public byte[] generatePassBundle(List<OSPass> Passes)
        {
            List<PassGeneratorRequest> requests = new List<PassGeneratorRequest>();
            foreach (OSPass p in Passes)
            {
                requests.Add(p);
            }
        }*/

        /// <summary>
        /// Helper method to initiate the pass with minimal information and certificates
        /// </summary>
        /// <param name="_genericInfo">Pass minimal information for generation </param>
        /// <param name="_certificates">Certificates to sign the passbook </param>
        /// <returns></returns>
        private PassGeneratorRequest generatePass(OSPassHeader _genericInfo, OSPassCertificates _certificates)
        {
            PassGenerator generator = new PassGenerator();
            PassGeneratorRequest passRequest = new PassGeneratorRequest();
            passRequest = setRequestHeaderInfo(_genericInfo);
            passRequest = setRequestCertificates(passRequest, _certificates);

            return passRequest;
        }

        private PassGeneratorRequest setRequestHeaderInfo(OSPassHeader _pass)
        {
            PassGeneratorRequest request = new PassGeneratorRequest();
            request.PassTypeIdentifier = _pass.passTypeIdentifier;
            request.TeamIdentifier = _pass.teamIdentifier;
            request.SerialNumber = _pass.serialNumber;
            request.Description = _pass.description;
            request.OrganizationName = _pass.organizationName;
            //request.LogoText = _pass.lo;
            return request;
        }

        private PassGeneratorRequest setRequestColor(PassGeneratorRequest request, OSPassColor _color)
        {
            request.BackgroundColor = _color.BackgroundColor;
            request.LabelColor = _color.LabelColor;
            request.ForegroundColor = _color.ForegroundColor;
            return request;
        }

        private PassGeneratorRequest setRequestCertificates(PassGeneratorRequest request, OSPassCertificates certificates)
        {
            request.AppleWWDRCACertificate = new X509Certificate2(certificates.AppleWWDRCACertificate);

            X509KeyStorageFlags flags = X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable;
            request.PassbookCertificate = new X509Certificate2(certificates.PassbookCertificate, certificates.PassbookCertificatePassword, flags); 
            
            return request;
        }

        private PassGeneratorRequest addIcons(PassGeneratorRequest request, OSPassImage _image)
        {
            request.Images.Add(PassbookImage.Icon, _image.image);
            request.Images.Add(PassbookImage.Icon2X, _image.image2x);
            request.Images.Add(PassbookImage.Icon3X, _image.image3x);
            return request;

        }

        private PassGeneratorRequest setPassStyle(PassGeneratorRequest _request, OSPassTypes _type, OSTransitTypes _tt)
        {
            switch (((int)_type))
            {
                case 0:
                    _request.Style = PassStyle.Generic;
                    _request.TransitType = getTransitType(_tt);
                    break;
                case 1:
                    _request.Style = PassStyle.BoardingPass;
                    break;
                case 2:
                    _request.Style = PassStyle.Coupon;
                    break;
                case 3:
                    _request.Style = PassStyle.EventTicket;
                    break;
                case 4:
                    _request.Style = PassStyle.StoreCard;
                    break;
                default:
                    throw new Exception();
            }
            return _request;
        }

        private TransitType getTransitType(OSTransitTypes _transitType)
        {
            switch (((int)_transitType))
            {
                case 0:
                    return TransitType.PKTransitTypeAir;
                case 1:
                    return TransitType.PKTransitTypeBoat;
                case 2:
                    return TransitType.PKTransitTypeBus;
                case 3:
                    return TransitType.PKTransitTypeGeneric;
                case 4: 
                    return TransitType.PKTransitTypeTrain;

            }
            throw new Exception();
        }

        private PassGeneratorRequest addPrimaryFields(PassGeneratorRequest _request, List<OSPassField> _fields)
        {
            foreach (OSPassField _field in _fields)
            {
                Field temp = new StandardField();
                temp.SetValue(_field.value);
                temp.Key = _field.key;
                temp.Label = _field.label;
                _request.AddPrimaryField(temp);
            }
            return _request;
        }

        private PassGeneratorRequest addSecondaryFields(PassGeneratorRequest _request, List<OSPassField> _fields)
        {
            foreach (OSPassField _field in _fields)
            {
                Field temp = new StandardField();
                temp.SetValue(_field.value);
                temp.Key = _field.key;
                temp.Label = _field.label;
                _request.AddSecondaryField(temp);
            }
            return _request;
        }

        private PassGeneratorRequest addAuxiliaryFields(PassGeneratorRequest _request, List<OSPassField> _fields)
        {
            foreach (OSPassField _field in _fields)
            {
                Field temp = new StandardField();
                temp.SetValue(_field.value);
                temp.Key = _field.key;
                temp.Label = _field.label;
                _request.AddAuxiliaryField(temp);
            }
            return _request;
        }

        private PassGeneratorRequest addBarCode(PassGeneratorRequest _request, string barCode)
        {
            _request.AddBarcode(BarcodeType.PKBarcodeFormatPDF417, barCode, "ISO-8859-1");
            return _request;
        }

        private PassGeneratorRequest addBackground(PassGeneratorRequest _request, OSPassImage _background)
        {

            _request.Images.Add(PassbookImage.Background, _background.image);
            _request.Images.Add(PassbookImage.Background2X, _background.image2x);
            _request.Images.Add(PassbookImage.Background3X, _background.image3x);
            return _request;
        }
        private PassGeneratorRequest addLogo(PassGeneratorRequest _request, OSPassImage _logo)
        {

            _request.Images.Add(PassbookImage.Logo, _logo.image);
            _request.Images.Add(PassbookImage.Logo2X, _logo.image2x);
            _request.Images.Add(PassbookImage.Logo3X, _logo.image3x);
            return _request;
        }

        private PassGeneratorRequest addThumbnail(PassGeneratorRequest _request, OSPassImage _thumbnail)
        {

            _request.Images.Add(PassbookImage.Thumbnail, _thumbnail.image);
            _request.Images.Add(PassbookImage.Thumbnail2X, _thumbnail.image2x);
            _request.Images.Add(PassbookImage.Thumbnail3X, _thumbnail.image3x);
            return _request;
        }

        private PassGeneratorRequest addExpireDates(PassGeneratorRequest _request, OSFullPass _pass)
        {
            _request.ExpirationDate = _pass.expireDate;
            return _request;
        }
    }
}
