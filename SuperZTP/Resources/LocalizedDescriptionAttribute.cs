using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace SuperZTP.Resources
{
    public class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        public LocalizedDescriptionAttribute(string resourceKey, Type resourceType)
        {
            if (resourceType != null)
            {
                var resourceManager = new ResourceManager(resourceType);
                DescriptionValue = resourceManager.GetString(resourceKey) ?? $"[{resourceKey}]";
            }
        }
    }
}
