using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSPassBook.Structures;
using OutSystems.ExternalLibraries.SDK;

namespace OSPassBook
{


    [OSInterface(Description ="This library generated PKPass to be added to the apple wallet", IconResourceName ="PassBook.Resources.Wallet.png")]
    public interface IOSPassGenerator
    {
        //public byte[] generatePassMinInfo(OSMinimalPass _pass);

        /// <summary>
        /// This method generates a Wallet Pass with allowing you to insert all data.
        /// All information in the header is mandatory as well as the certificates.
        /// 
        /// For more info on the mandatory data check the official documentation.
        /// </summary>
        /// <param name="_passs">This is the structure that hols all pass information</param>
        /// <returns></returns>
        public byte[] generatePassFullInfo(OSFullPass _passs);


        //public byte[] generatePassBundle(List<OSPass> Passes);
    }
}
