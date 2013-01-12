using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ailon.WP.Utils
{
    public static class PhoneNameResolver
    {
        public static CanonicalPhoneName Resolve(string manufacturer, string model)
        {
            var manufacturerNormalized = manufacturer.Trim().ToUpper();

            switch (manufacturerNormalized)
            {
                case "NOKIA":
                    return ResolveNokia(manufacturer, model);
                default:
                    return new CanonicalPhoneName()
                    {
                        ReportedManufacturer = manufacturer,
                        ReportedModel = model,
                        CanonicalManufacturer = manufacturer,
                        CanonicalModel = model,
                        IsResolved = false
                    };

            }
        }

        private static CanonicalPhoneName ResolveNokia(string manufacturer, string model)
        {
            var modelNormalized = model.Trim().ToUpper();

            var result = new CanonicalPhoneName()
            {
                ReportedManufacturer = manufacturer,
                ReportedModel = model,
                CanonicalManufacturer = "NOKIA",
                CanonicalModel = model,
                IsResolved = false
            };

            var lookupValue = modelNormalized;
            if (modelNormalized.StartsWith("RM-"))
            {
                lookupValue = modelNormalized.Substring(0, 6);
            }

            if (nokiaLookupTable.ContainsKey(lookupValue))
            {
                var modelMetadata = nokiaLookupTable[lookupValue];
                if (nokiaCanonicalModels.ContainsKey(modelMetadata.ModelId))
                {
                    result.CanonicalManufacturer = nokiaCanonicalModels[modelMetadata.ModelId].CanonicalManufacturer;
                    result.CanonicalModel = nokiaCanonicalModels[modelMetadata.ModelId].CanonicalModel;
                    result.Comments = modelMetadata.Description;
                    result.IsResolved = true;
                }
            }

            return result;
        }

        private static Dictionary<string, CanonicalPhoneName> nokiaCanonicalModels = new Dictionary<string, CanonicalPhoneName>()
        {
            { "Lumia920", new CanonicalPhoneName() { CanonicalManufacturer = "NOKIA", CanonicalModel = "Lumia 920" } } 
        };

        private static Dictionary<string, ReportedNameMetadata> nokiaLookupTable = new Dictionary<string, ReportedNameMetadata>()
        {
            // Lumia 920
            { "RM-820", new ReportedNameMetadata("Lumia920", null) },
            { "RM-821", new ReportedNameMetadata("Lumia920", null) },
            { "RM-822", new ReportedNameMetadata("Lumia920", "China") },
            { "RM-867", new ReportedNameMetadata("Lumia920", "920T") },
        };
    }

    public class CanonicalPhoneName
    {
        public string ReportedManufacturer { get; set; }
        public string ReportedModel { get; set; }
        public string CanonicalManufacturer { get; set; }
        public string CanonicalModel { get; set; }
        public string Comments { get; set; }
        public bool IsResolved { get; set; }
    }

    class ReportedNameMetadata
    {
        internal string ModelId { get; set; }
        internal string Description { get; set; }

        internal ReportedNameMetadata(string modelId, string description)
        {
            this.ModelId = modelId;
            this.Description = description;
        }
    }
}
