using Piranha;
using Piranha.Extend;
using Piranha.Manager;
using Piranha.Security;

namespace GangsterGeek.Piranha.AuthSystem
{
    public class AuthModule : IModule
    {
        private readonly List<PermissionItem> _permissions = new List<PermissionItem>
        {
        };

        /// <summary>
        /// Gets the module author
        /// </summary>
        public string Author => "";

        /// <summary>
        /// Gets the module name
        /// </summary>
        public string Name => "";

        /// <summary>
        /// Gets the module version
        /// </summary>
        public string Version => Utils.GetAssemblyVersion(GetType().Assembly);

        /// <summary>
        /// Gets the module description
        /// </summary>
        public string Description => "";

        /// <summary>
        /// Gets the module package url
        /// </summary>
        public string PackageUrl => "";

        /// <summary>
        /// Gets the module icon url
        /// </summary>
        public string IconUrl => "/manager/PiranhaModule/piranha-logo.png";

        public void Init()
        {
        }
    }
}
