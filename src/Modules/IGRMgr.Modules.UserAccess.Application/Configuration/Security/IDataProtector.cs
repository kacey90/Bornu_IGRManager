using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Application.Configuration.Security
{
    public interface IDataProtector
    {
        string Encrypt(string plainText);

        string Decrypt(string encryptedText);
    }
}
