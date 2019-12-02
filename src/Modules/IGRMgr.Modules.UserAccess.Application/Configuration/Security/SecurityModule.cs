using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Application.Configuration.Security
{
    internal class SecurityModule : Module
    {
        private readonly string _encryptionKey;

        public SecurityModule(string encryptionKey)
        {
            _encryptionKey = encryptionKey;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AesDataProtector>()
                .As<IDataProtector>()
                .WithParameter("encryptionKey", _encryptionKey);
        }
    }
}
