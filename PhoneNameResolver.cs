﻿/*
 * Copyright (c) 2013 by Alan Mendelevich
 * 
 * Licensed under MIT license.
 * 
 * See license.txt for details.
*/

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
                case "HTC":
                    return ResolveHtc(manufacturer, model);
                case "SAMSUNG":
                    return ResolveSamsung(manufacturer, model);
                case "LG":
                    return ResolveLg(manufacturer, model);
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

        private static CanonicalPhoneName ResolveLg(string manufacturer, string model)
        {
            var modelNormalized = model.Trim().ToUpper();

            var result = new CanonicalPhoneName()
            {
                ReportedManufacturer = manufacturer,
                ReportedModel = model,
                CanonicalManufacturer = "LG",
                CanonicalModel = model,
                IsResolved = false
            };


            var lookupValue = modelNormalized;

            if (lookupValue.StartsWith("LG-C900"))
            {
                lookupValue = "LG-C900";
            }

            if (lookupValue.StartsWith("LG-E900"))
            {
                lookupValue = "LG-E900";
            }

            if (lgLookupTable.ContainsKey(lookupValue))
            {
                var modelMetadata = lgLookupTable[lookupValue];
                result.CanonicalModel = modelMetadata.CanonicalModel;
                result.Comments = modelMetadata.Comments;
                result.IsResolved = true;
            }

            return result;
        }

        private static CanonicalPhoneName ResolveSamsung(string manufacturer, string model)
        {
            var modelNormalized = model.Trim().ToUpper();

            var result = new CanonicalPhoneName()
            {
                ReportedManufacturer = manufacturer,
                ReportedModel = model,
                CanonicalManufacturer = "SAMSUNG",
                CanonicalModel = model,
                IsResolved = false
            };


            var lookupValue = modelNormalized;

            if (lookupValue.StartsWith("GT-S7530"))
            {
                lookupValue = "GT-S7530";
            }

            if (lookupValue.StartsWith("SGH-I917"))
            {
                lookupValue = "SGH-I917";
            }

            if (samsungLookupTable.ContainsKey(lookupValue))
            {
                var modelMetadata = samsungLookupTable[lookupValue];
                result.CanonicalModel = modelMetadata.CanonicalModel;
                result.Comments = modelMetadata.Comments;
                result.IsResolved = true;
            }

            return result;
        }

        private static CanonicalPhoneName ResolveHtc(string manufacturer, string model)
        {
            var modelNormalized = model.Trim().ToUpper();

            var result = new CanonicalPhoneName()
            {
                ReportedManufacturer = manufacturer,
                ReportedModel = model,
                CanonicalManufacturer = "HTC",
                CanonicalModel = model,
                IsResolved = false
            };


            var lookupValue = modelNormalized;

            if (lookupValue.StartsWith("A620"))
            {
                lookupValue = "A620";
            }

            if (lookupValue.StartsWith("C625"))
            {
                lookupValue = "C625";
            }

            if (lookupValue.StartsWith("C620"))
            {
                lookupValue = "C625";
            }

            if (htcLookupTable.ContainsKey(lookupValue))
            {
                var modelMetadata = htcLookupTable[lookupValue];
                result.CanonicalModel = modelMetadata.CanonicalModel;
                result.Comments = modelMetadata.Comments;
                result.IsResolved = true;
            }

            return result;
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
                    result.CanonicalModel = modelMetadata.CanonicalModel;
                    result.Comments = modelMetadata.Comments;
                    result.IsResolved = true;
            }

            return result;
        }

        private static Dictionary<string, CanonicalPhoneName> lgLookupTable = new Dictionary<string, CanonicalPhoneName>()
        {
            // Optimus 7Q/Quantum
            { "LG-C900", new CanonicalPhoneName() { CanonicalModel = "Optimus 7Q/Quantum" } },

            // Optimus 7
            { "LG-E900", new CanonicalPhoneName() { CanonicalModel = "Optimus 7" } },

            // Jil Sander
            { "LG-E906", new CanonicalPhoneName() { CanonicalModel = "Jil Sander" } },
        };

        private static Dictionary<string, CanonicalPhoneName> samsungLookupTable = new Dictionary<string, CanonicalPhoneName>()
        {
            // OMNIA W
            { "GT-I8350", new CanonicalPhoneName() { CanonicalModel = "Omnia W" } },
            { "GT-I8350T", new CanonicalPhoneName() { CanonicalModel = "Omnia W" } },
            { "OMNIA W", new CanonicalPhoneName() { CanonicalModel = "Omnia W" } },

            // OMNIA 7
            { "GT-I8700", new CanonicalPhoneName() { CanonicalModel = "Omnia 7" } },
            { "OMNIA7", new CanonicalPhoneName() { CanonicalModel = "Omnia 7" } },

            // OMNIA M
            { "GT-S7530", new CanonicalPhoneName() { CanonicalModel = "Omnia 7" } },

            // Focus
            { "I917", new CanonicalPhoneName() { CanonicalModel = "Focus" } },
            { "SGH-I917", new CanonicalPhoneName() { CanonicalModel = "Focus" } },

            // Focus 2
            { "SGH-I667", new CanonicalPhoneName() { CanonicalModel = "Focus 2" } },

            // Focus Flash
            { "SGH-I677", new CanonicalPhoneName() { CanonicalModel = "Focus Flash" } },

            // Focus S
            { "HADEN", new CanonicalPhoneName() { CanonicalModel = "Focus S" } },
            { "SGH-I937", new CanonicalPhoneName() { CanonicalModel = "Focus S" } },

            // ATIV S
            { "GT-I8750", new CanonicalPhoneName() { CanonicalModel = "ATIV S" } },
            { "SGH-T899M", new CanonicalPhoneName() { CanonicalModel = "ATIV S" } },

            // ATIV Odyssey
            { "SCH-I930", new CanonicalPhoneName() { CanonicalModel = "ATIV Odyssey" } },
        };

        private static Dictionary<string, CanonicalPhoneName> htcLookupTable = new Dictionary<string, CanonicalPhoneName>()
        {
            // Surround
            { "7 MONDRIAN T8788", new CanonicalPhoneName() { CanonicalModel = "Surround" } },
            { "T8788", new CanonicalPhoneName() { CanonicalModel = "Surround" } },
            { "SURROUND", new CanonicalPhoneName() { CanonicalModel = "Surround" } },
            { "SURROUND T8788", new CanonicalPhoneName() { CanonicalModel = "Surround" } },

            // Mozart
            { "7 MOZART", new CanonicalPhoneName() { CanonicalModel = "Mozart" } },
            { "7 MOZART T8698", new CanonicalPhoneName() { CanonicalModel = "Mozart" } },
            { "HTC MOZART", new CanonicalPhoneName() { CanonicalModel = "Mozart" } },
            { "MERSAD 7 MOZART T8698", new CanonicalPhoneName() { CanonicalModel = "Mozart" } },
            { "MOZART", new CanonicalPhoneName() { CanonicalModel = "Mozart" } },
            { "MOZART T8698", new CanonicalPhoneName() { CanonicalModel = "Mozart" } },
            { "PD67100", new CanonicalPhoneName() { CanonicalModel = "Mozart" } },
            { "T8697", new CanonicalPhoneName() { CanonicalModel = "Mozart" } },

            // Pro
            { "7 PRO T7576", new CanonicalPhoneName() { CanonicalModel = "7 Pro" } },
            { "MWP6885", new CanonicalPhoneName() { CanonicalModel = "7 Pro" } },
            { "USCCHTC-PC93100", new CanonicalPhoneName() { CanonicalModel = "7 Pro" } },

            // Arrive
            { "PC93100", new CanonicalPhoneName() { CanonicalModel = "Arrive", Comments = "Sprint" } },
            { "T7575", new CanonicalPhoneName() { CanonicalModel = "Arrive", Comments = "Sprint" } },

            // HD2
            { "HD2", new CanonicalPhoneName() { CanonicalModel = "HD2" } },
            { "HD2 LEO", new CanonicalPhoneName() { CanonicalModel = "HD2" } },
            { "LEO", new CanonicalPhoneName() { CanonicalModel = "HD2" } },

            // HD7
            { "7 SCHUBERT T9292", new CanonicalPhoneName() { CanonicalModel = "HD7" } },
            { "GOLD", new CanonicalPhoneName() { CanonicalModel = "HD7" } },
            { "HD7", new CanonicalPhoneName() { CanonicalModel = "HD7" } },
            { "HD7 T9292", new CanonicalPhoneName() { CanonicalModel = "HD7" } },
            { "MONDRIAN", new CanonicalPhoneName() { CanonicalModel = "HD7" } },
            { "SCHUBERT", new CanonicalPhoneName() { CanonicalModel = "HD7" } },
            { "Schubert T9292", new CanonicalPhoneName() { CanonicalModel = "HD7" } },
            { "T9296", new CanonicalPhoneName() { CanonicalModel = "HD7", Comments = "Telstra, AU" } },
            { "TOUCH-IT HD7", new CanonicalPhoneName() { CanonicalModel = "HD7" } },

            // HD7S
            { "T9295", new CanonicalPhoneName() { CanonicalModel = "HD7S" } },

            // Trophy
            { "7 TROPHY", new CanonicalPhoneName() { CanonicalModel = "Trophy" } },
            { "7 TROPHY T8686", new CanonicalPhoneName() { CanonicalModel = "Trophy" } },
            { "PC40100", new CanonicalPhoneName() { CanonicalModel = "Trophy", Comments = "Verizon" } },
            { "SPARK", new CanonicalPhoneName() { CanonicalModel = "Trophy" } },
            { "TOUCH-IT TROPHY", new CanonicalPhoneName() { CanonicalModel = "Trophy" } },
            { "MWP6985", new CanonicalPhoneName() { CanonicalModel = "Trophy" } },

            // 8S
            { "A620", new CanonicalPhoneName() { CanonicalModel = "8S" } },
            { "WINDOWS PHONE 8S BY HTC", new CanonicalPhoneName() { CanonicalModel = "8S" } },

            // 8X
            { "C620", new CanonicalPhoneName() { CanonicalModel = "8X" } },
            { "C625", new CanonicalPhoneName() { CanonicalModel = "8X" } },
            { "HTC6990LVW", new CanonicalPhoneName() { CanonicalModel = "8X", Comments="Verizon" } },
            { "PM23300", new CanonicalPhoneName() { CanonicalModel = "8X", Comments="AT&T" } },
            { "WINDOWS PHONE 8X BY HTC", new CanonicalPhoneName() { CanonicalModel = "8X" } },

            // Titan
            { "ETERNITY", new CanonicalPhoneName() { CanonicalModel = "Titan", Comments = "China" } },
            { "PI39100", new CanonicalPhoneName() { CanonicalModel = "Titan", Comments = "AT&T" } },
            { "TITAN X310E", new CanonicalPhoneName() { CanonicalModel = "Titan" } },
            { "ULTIMATE", new CanonicalPhoneName() { CanonicalModel = "Titan" } },
            { "X310E", new CanonicalPhoneName() { CanonicalModel = "Titan" } },
            { "X310E TITAN", new CanonicalPhoneName() { CanonicalModel = "Titan" } },
            
            // Titan II
            { "PI86100", new CanonicalPhoneName() { CanonicalModel = "Titan II", Comments = "AT&T" } },
            { "RADIANT", new CanonicalPhoneName() { CanonicalModel = "Titan II" } },

            // Radar
            { "RADAR", new CanonicalPhoneName() { CanonicalModel = "Radar" } },
            { "RADAR 4G", new CanonicalPhoneName() { CanonicalModel = "Radar", Comments = "T-Mobile USA" } },
            { "RADAR C110E", new CanonicalPhoneName() { CanonicalModel = "Radar" } },
            
        };

        private static Dictionary<string, CanonicalPhoneName> nokiaLookupTable = new Dictionary<string, CanonicalPhoneName>()
        {
            // Lumia 505
            { "LUMIA 505", new CanonicalPhoneName() { CanonicalModel = "Lumia 505" } },
            // Lumia 510
            { "LUMIA 510", new CanonicalPhoneName() { CanonicalModel = "Lumia 510" } },
            { "NOKIA 510", new CanonicalPhoneName() { CanonicalModel = "Lumia 510" } },
            // Lumia 610
            { "LUMIA 610", new CanonicalPhoneName() { CanonicalModel = "Lumia 610" } },
            { "LUMIA 610 NFC", new CanonicalPhoneName() { CanonicalModel = "Lumia 610", Comments = "NFC" } },
            { "NOKIA 610", new CanonicalPhoneName() { CanonicalModel = "Lumia 610" } },
            { "NOKIA 610C", new CanonicalPhoneName() { CanonicalModel = "Lumia 610" } },
            // Lumia 620
            { "LUMIA 620", new CanonicalPhoneName() { CanonicalModel = "Lumia 620" } },
            { "RM-846", new CanonicalPhoneName() { CanonicalModel = "Lumia 620" } },
            // Lumia 710
            { "LUMIA 710", new CanonicalPhoneName() { CanonicalModel = "Lumia 710" } },
            { "NOKIA 710", new CanonicalPhoneName() { CanonicalModel = "Lumia 710" } },
            // Lumia 800
            { "LUMIA 800", new CanonicalPhoneName() { CanonicalModel = "Lumia 800" } },
            { "LUMIA 800C", new CanonicalPhoneName() { CanonicalModel = "Lumia 800" } },
            { "NOKIA 800", new CanonicalPhoneName() { CanonicalModel = "Lumia 800" } },
            { "NOKIA 800C", new CanonicalPhoneName() { CanonicalModel = "Lumia 800", Comments = "China" } },
            // Lumia 810
            { "RM-878", new CanonicalPhoneName() { CanonicalModel = "Lumia 810" } },
            // Lumia 820
            { "RM-824", new CanonicalPhoneName() { CanonicalModel = "Lumia 820" } },
            { "RM-825", new CanonicalPhoneName() { CanonicalModel = "Lumia 820" } },
            { "RM-826", new CanonicalPhoneName() { CanonicalModel = "Lumia 820" } },
            // Lumia 822
            { "RM-845", new CanonicalPhoneName() { CanonicalModel = "Lumia 822", Comments = "Verizon" } },
            // Lumia 900
            { "LUMIA 900", new CanonicalPhoneName() { CanonicalModel = "Lumia 900" } },
            { "NOKIA 900", new CanonicalPhoneName() { CanonicalModel = "Lumia 900" } },
            // Lumia 920
            { "RM-820", new CanonicalPhoneName() { CanonicalModel = "Lumia 920" } },
            { "RM-821", new CanonicalPhoneName() { CanonicalModel = "Lumia 920" } },
            { "RM-822", new CanonicalPhoneName() { CanonicalModel = "Lumia 920" } },
            { "RM-867", new CanonicalPhoneName() { CanonicalModel = "Lumia 920", Comments = "920T" } },
            { "NOKIA 920", new CanonicalPhoneName() { CanonicalModel = "Lumia 920" } },
            { "LUMIA 920", new CanonicalPhoneName() { CanonicalModel = "Lumia 920" } },
            // Lumia 520
            { "RM-914", new CanonicalPhoneName() { CanonicalModel = "Lumia 520" } },
            { "RM-915", new CanonicalPhoneName() { CanonicalModel = "Lumia 520" } },
            { "RM-913", new CanonicalPhoneName() { CanonicalModel = "Lumia 520", Comments="520T" } },
            // Lumia 521?
            { "RM-917", new CanonicalPhoneName() { CanonicalModel = "Lumia 521", Comments="T-Mobile 520" } },
            // Lumia 720
            { "RM-885", new CanonicalPhoneName() { CanonicalModel = "Lumia 720" } },
            { "RM-887", new CanonicalPhoneName() { CanonicalModel = "Lumia 720", Comments="China 720T" } },
            // Lumia 928
            { "RM-860", new CanonicalPhoneName() { CanonicalModel = "Lumia 928" } },
            // Lumia 925
            { "RM-892", new CanonicalPhoneName() { CanonicalModel = "Lumia 925" } },
            { "RM-893", new CanonicalPhoneName() { CanonicalModel = "Lumia 925" } },
            { "RM-910", new CanonicalPhoneName() { CanonicalModel = "Lumia 925", Comments="China 925T" } },
            // Lumia 1020
            { "RM-875", new CanonicalPhoneName() { CanonicalModel = "Lumia 1020" } },
            { "RM-876", new CanonicalPhoneName() { CanonicalModel = "Lumia 1020" } },
            { "RM-877", new CanonicalPhoneName() { CanonicalModel = "Lumia 1020" } },
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

        public string FullCanonicalName
        {
            get { return CanonicalManufacturer + " " + CanonicalModel; }
        }
    }

}
