using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using XY.Services;
using XY.Services.Weixin;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            //signature=cb40d15f2a5cddb6b7951b543d485922198b9b0b&timestamp=1456443100&nonce=1898623024&encrypt_type=aes&msg_signature=c923de30d01ddd929c927e74a13e4b66b77f1b48
            var wx_open = wx_openInfoService.instance().Single(new Guid("477F0554-837C-4D10-9C12-3DFE44B8DD60"));
            var Company = CompanyService.instance().Single(new Guid("02A07495-5484-4162-A70D-B7341096A1D4"));


            var result = ComponentApi.GetComponentAccessToken(wx_open.open_sAppID, wx_open.open_sAppSecret, wx_open.open_ticket);
            //mUqPNgfa8cNXYDWoKAoJL2xwcaS5EY_0Go0HS_3c4JOJ045Cml3zFjRFftbG-z1rMJY6DbpOEwGoXwvuIvHjmESbTb1HCVHsUs7f2IW95WoHOBjACAMTG

            var coderesult = ComponentApi.GetPreAuthCode(wx_open.open_sAppID, result.component_access_token);


            Console.WriteLine();
        }



    }

}
