using System;
using System.Text;

namespace BOSSAutoRef.Data
{
    public static class ReferenceData
    {
        public static string GetBrokerageStatusValues()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("[");
            stringBuilder.AppendLine("    {");
            stringBuilder.AppendLine("        \"Code\": \"ABK\",");
            stringBuilder.AppendLine("        \"Description\": \"Alternate Broker\",");
            stringBuilder.AppendLine("        \"CodeAndDescription\": \"ABK (Alternate Broker)\",");
            stringBuilder.AppendLine("        \"Type\": \"CT\",");
            stringBuilder.AppendLine("        \"ProcessingCountry\": \"CN\",");
            stringBuilder.AppendLine("        \"OriginalDescription\": \"Alternate Broker\"");
            stringBuilder.AppendLine("    },");
            stringBuilder.AppendLine("    {");
            stringBuilder.AppendLine("        \"Code\": \"DEM\",");
            stringBuilder.AppendLine("        \"Description\": \"Deminimis\",");
            stringBuilder.AppendLine("        \"CodeAndDescription\": \"DEM (Deminimis)\",");
            stringBuilder.AppendLine("        \"Type\": \"CT\",");
            stringBuilder.AppendLine("        \"ProcessingCountry\": \"CN\",");
            stringBuilder.AppendLine("        \"OriginalDescription\": \"Deminimis\"");
            stringBuilder.AppendLine("    },");
            stringBuilder.AppendLine("    {");
            stringBuilder.AppendLine("        \"Code\": \"ECS\",");
            stringBuilder.AppendLine("        \"Description\": \"外海电商\",");
            stringBuilder.AppendLine("        \"CodeAndDescription\": \"ECS (外海电商)\",");
            stringBuilder.AppendLine("        \"Type\": \"CT\",");
            stringBuilder.AppendLine("        \"ProcessingCountry\": \"CN\",");
            stringBuilder.AppendLine("        \"OriginalDescription\": \"e-Commerce\"");
            stringBuilder.AppendLine("    },");
            stringBuilder.AppendLine("    {");
            stringBuilder.AppendLine("        \"Code\": \"FML\",");
            stringBuilder.AppendLine("        \"Description\": \"Formal Entry\",");
            stringBuilder.AppendLine("        \"CodeAndDescription\": \"FML (Formal Entry)\",");
            stringBuilder.AppendLine("        \"Type\": \"CT\",");
            stringBuilder.AppendLine("        \"ProcessingCountry\": \"CN\",");
            stringBuilder.AppendLine("        \"OriginalDescription\": \"Formal Entry\"");
            stringBuilder.AppendLine("    },");
            stringBuilder.AppendLine("    {");
            stringBuilder.AppendLine("        \"Code\": \"FTZ\",");
            stringBuilder.AppendLine("        \"Description\": \"Free Zone\",");
            stringBuilder.AppendLine("        \"CodeAndDescription\": \"FTZ (Free Zone)\",");
            stringBuilder.AppendLine("        \"Type\": \"CT\",");
            stringBuilder.AppendLine("        \"ProcessingCountry\": \"CN\",");
            stringBuilder.AppendLine("        \"OriginalDescription\": \"Free Zone\"");
            stringBuilder.AppendLine("    },");
            stringBuilder.AppendLine("    {");
            stringBuilder.AppendLine("        \"Code\": \"FZT\",");
            stringBuilder.AppendLine("        \"Description\": \"FreeTrade Zone Transship\",");
            stringBuilder.AppendLine("        \"CodeAndDescription\": \"FZT (FreeTrade Zone Transship)\",");
            stringBuilder.AppendLine("        \"Type\": \"CT\",");
            stringBuilder.AppendLine("        \"ProcessingCountry\": \"CN\",");
            stringBuilder.AppendLine("        \"OriginalDescription\": \"FreeTrade Zone Transship\"");
            stringBuilder.AppendLine("    },");
            stringBuilder.AppendLine("    {");
            stringBuilder.AppendLine("        \"Code\": \"FZW\",");
            stringBuilder.AppendLine("        \"Description\": \"FreeTrade Zone Warehouse\",");
            stringBuilder.AppendLine("        \"CodeAndDescription\": \"FZW (FreeTrade Zone Warehouse)\",");
            stringBuilder.AppendLine("        \"Type\": \"CT\",");
            stringBuilder.AppendLine("        \"ProcessingCountry\": \"CN\",");
            stringBuilder.AppendLine("        \"OriginalDescription\": \"FreeTrade Zone Warehouse\"");
            stringBuilder.AppendLine("    },");
            stringBuilder.AppendLine("    {");
            stringBuilder.AppendLine("        \"Code\": \"IDM\",");
            stringBuilder.AppendLine("        \"Description\": \"Informal Deminimis\",");
            stringBuilder.AppendLine("        \"CodeAndDescription\": \"IDM (Informal Deminimis)\",");
            stringBuilder.AppendLine("        \"Type\": \"CT\",");
            stringBuilder.AppendLine("        \"ProcessingCountry\": \"CN\",");
            stringBuilder.AppendLine("        \"OriginalDescription\": \"Informal Deminimis\"");
            stringBuilder.AppendLine("    },");
            stringBuilder.AppendLine("    {");
            stringBuilder.AppendLine("        \"Code\": \"IDT\",");
            stringBuilder.AppendLine("        \"Description\": \"Informal Dutiable\",");
            stringBuilder.AppendLine("        \"CodeAndDescription\": \"IDT (Informal Dutiable)\",");
            stringBuilder.AppendLine("        \"Type\": \"CT\",");
            stringBuilder.AppendLine("        \"ProcessingCountry\": \"CN\",");
            stringBuilder.AppendLine("        \"OriginalDescription\": \"Informal Dutiable\"");
            stringBuilder.AppendLine("    },");
            stringBuilder.AppendLine("    {");
            stringBuilder.AppendLine("        \"Code\": \"INF\",");
            stringBuilder.AppendLine("        \"Description\": \"Informal\",");
            stringBuilder.AppendLine("        \"CodeAndDescription\": \"INF (Informal)\",");
            stringBuilder.AppendLine("        \"Type\": \"CT\",");
            stringBuilder.AppendLine("        \"ProcessingCountry\": \"CN\",");
            stringBuilder.AppendLine("        \"OriginalDescription\": \"Informal\"");
            stringBuilder.AppendLine("    },");
            stringBuilder.AppendLine("    {");
            stringBuilder.AppendLine("        \"Code\": \"LND\",");
            stringBuilder.AppendLine("        \"Description\": \"Letter/Doc\",");
            stringBuilder.AppendLine("        \"CodeAndDescription\": \"LND (Letter/Doc)\",");
            stringBuilder.AppendLine("        \"Type\": \"CT\",");
            stringBuilder.AppendLine("        \"ProcessingCountry\": \"CN\",");
            stringBuilder.AppendLine("        \"OriginalDescription\": \"Letter/Doc\"");
            stringBuilder.AppendLine("    },");
            stringBuilder.AppendLine("    {");
            stringBuilder.AppendLine("        \"Code\": \"PGS\",");
            stringBuilder.AppendLine("        \"Description\": \"Personal goods\",");
            stringBuilder.AppendLine("        \"CodeAndDescription\": \"PGS (Personal goods)\",");
            stringBuilder.AppendLine("        \"Type\": \"CT\",");
            stringBuilder.AppendLine("        \"ProcessingCountry\": \"CN\",");
            stringBuilder.AppendLine("        \"OriginalDescription\": \"Personal goods\"");
            stringBuilder.AppendLine("    },");
            stringBuilder.AppendLine("    {");
            stringBuilder.AppendLine("        \"Code\": \"UDB\",");
            stringBuilder.AppendLine("        \"Description\": \"Underbond\",");
            stringBuilder.AppendLine("        \"CodeAndDescription\": \"UDB (Underbond)\",");
            stringBuilder.AppendLine("        \"Type\": \"CT\",");
            stringBuilder.AppendLine("        \"ProcessingCountry\": \"CN\",");
            stringBuilder.AppendLine("        \"OriginalDescription\": \"Underbond\"");
            stringBuilder.AppendLine("    },");
            stringBuilder.AppendLine("    {");
            stringBuilder.AppendLine("        \"Code\": \"ULS\",");
            stringBuilder.AppendLine("        \"Description\": \"Unaccompanied Luggage\",");
            stringBuilder.AppendLine("        \"CodeAndDescription\": \"ULS (Unaccompanied Luggage)\",");
            stringBuilder.AppendLine("        \"Type\": \"CT\",");
            stringBuilder.AppendLine("        \"ProcessingCountry\": \"CN\",");
            stringBuilder.AppendLine("        \"OriginalDescription\": \"Unaccompanied Luggage\"");
            stringBuilder.AppendLine("    },");
            stringBuilder.AppendLine("    {");
            stringBuilder.AppendLine("        \"Code\": \"UND\",");
            stringBuilder.AppendLine("        \"Description\": \"Undefined\",");
            stringBuilder.AppendLine("        \"CodeAndDescription\": \"UND (Undefined)\",");
            stringBuilder.AppendLine("        \"Type\": \"CT\",");
            stringBuilder.AppendLine("        \"ProcessingCountry\": \"CN\",");
            stringBuilder.AppendLine("        \"OriginalDescription\": \"Undefined\"");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("]");
            return stringBuilder.ToString();
        }

        public static string GetLocalLookupCodes()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("[");
            stringBuilder.AppendLine("    {");
            stringBuilder.AppendLine("        \"Code\": \"F_CLEAR\",");
            stringBuilder.AppendLine("        \"Description\": \"ABK|DEM|FML|INF|LND|PGS|ULS|UND\",");
            stringBuilder.AppendLine("        \"Type\": \"LPM\",");
            stringBuilder.AppendLine("        \"CodeAndDescription\": \"F_CLEAR (ABK|DEM|FML|INF|LND|PGS|ULS|UND)\",");
            stringBuilder.AppendLine("        \"OriginalDescription\": \"ABK|DEM|FML|INF|LND|PGS|ULS|UND\"");
            stringBuilder.AppendLine("    },");
            stringBuilder.AppendLine("    {");
            stringBuilder.AppendLine("        \"Code\": \"F_CUSTOM\",");
            stringBuilder.AppendLine("        \"Description\": \"NEW|RFC|INS|ERR\",");
            stringBuilder.AppendLine("        \"Type\": \"LPM\",");
            stringBuilder.AppendLine("        \"CodeAndDescription\": \"F_CUSTOM (NEW|RFC|INS|ERR)\",");
            stringBuilder.AppendLine("        \"OriginalDescription\": \"NEW|RFC|INS|ERR\"");
            stringBuilder.AppendLine("    },");
            stringBuilder.AppendLine("    {");
            stringBuilder.AppendLine("        \"Code\": \"F_RATING\",");
            stringBuilder.AppendLine("        \"Description\": \"ABN|CMP|RNR|RNS\",");
            stringBuilder.AppendLine("        \"Type\": \"LPM\",");
            stringBuilder.AppendLine("        \"CodeAndDescription\": \"F_RATING (ABN|CMP|RNR|RNS)\",");
            stringBuilder.AppendLine("        \"OriginalDescription\": \"ABN|CMP|RNR|RNS\"");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("]");
            return stringBuilder.ToString();
        }

        public static string GetMeasurementUnits()
        {
            return
            "[" + Environment.NewLine +
            "    {" + Environment.NewLine +
            "        \"Code\": \"KG\"," + Environment.NewLine +
            "        \"Description\": \"KILOGRAM\"," + Environment.NewLine +
            "        \"UPSCode\": \"KG \"," + Environment.NewLine +
            "        \"UPSDescription\": \"KILOGRAM\"," + Environment.NewLine +
            "        \"CustomsCode\": \"KGE\"," + Environment.NewLine +
            "        \"CustomsDescription\": \"Eigengewicht in Kilogramm\"," + Environment.NewLine +
            "        \"OriginalDescription\": \"KILOGRAM\"" + Environment.NewLine +
            "    }," + Environment.NewLine +
            "    {" + Environment.NewLine +
            "        \"Code\": \"KGA\"," + Environment.NewLine +
            "        \"Description\": \"Kilogramm reiner Alkohol\"," + Environment.NewLine +
            "        \"UPSCode\": \" \"," + Environment.NewLine +
            "        \"UPSDescription\": \" \"," + Environment.NewLine +
            "        \"CustomsCode\": \"KGA\"," + Environment.NewLine +
            "        \"CustomsDescription\": \"Kilogramm reiner Alkohol\"," + Environment.NewLine +
            "        \"OriginalDescription\": \"Kilogramm reiner Alkohol\"" + Environment.NewLine +
            "    }," + Environment.NewLine +
            "    {" + Environment.NewLine +
            "        \"Code\": \"KGB\"," + Environment.NewLine +
            "        \"Description\": \"Rohgewicht in Kilogramm\"," + Environment.NewLine +
            "        \"UPSCode\": \" \"," + Environment.NewLine +
            "        \"UPSDescription\": \" \"," + Environment.NewLine +
            "        \"CustomsCode\": \"KGB\"," + Environment.NewLine +
            "        \"CustomsDescription\": \"Rohgewicht in Kilogramm\"," + Environment.NewLine +
            "        \"OriginalDescription\": \"Rohgewicht in Kilogramm\"" + Environment.NewLine +
            "    }," + Environment.NewLine +
            "    {" + Environment.NewLine +
            "        \"Code\": \"KGD\"," + Environment.NewLine +
            "        \"Description\": \"Eigenwicht des Biodieselgehalts in \"," + Environment.NewLine +
            "        \"UPSCode\": \" \"," + Environment.NewLine +
            "        \"UPSDescription\": \" \"," + Environment.NewLine +
            "        \"CustomsCode\": \"KGD\"," + Environment.NewLine +
            "        \"CustomsDescription\": \"Eigenwicht des Biodieselgehalts in \"," + Environment.NewLine +
            "        \"OriginalDescription\": \"Eigenwicht des Biodieselgehalts in \"" + Environment.NewLine +
            "    }," + Environment.NewLine +
            "    {" + Environment.NewLine +
            "        \"Code\": \"KGF\"," + Environment.NewLine +
            "        \"Description\": \"Eigenwicht der Brennmasse in Kilogr\"," + Environment.NewLine +
            "        \"UPSCode\": \" \"," + Environment.NewLine +
            "        \"UPSDescription\": \" \"," + Environment.NewLine +
            "        \"CustomsCode\": \"KGF\"," + Environment.NewLine +
            "        \"CustomsDescription\": \"Eigenwicht der Brennmasse in Kilogr\"," + Environment.NewLine +
            "        \"OriginalDescription\": \"Eigenwicht der Brennmasse in Kilogr\"" + Environment.NewLine +
            "    }," + Environment.NewLine +
            "    {" + Environment.NewLine +
            "        \"Code\": \"KGG\"," + Environment.NewLine +
            "        \"Description\": \"KILOGRAM GROSS\"," + Environment.NewLine +
            "        \"UPSCode\": \"KGG\"," + Environment.NewLine +
            "        \"UPSDescription\": \"KILOGRAM GROSS\"," + Environment.NewLine +
            "        \"CustomsCode\": \" \"," + Environment.NewLine +
            "        \"CustomsDescription\": \" \"," + Environment.NewLine +
            "        \"OriginalDescription\": \"KILOGRAM GROSS\"" + Environment.NewLine +
            "    }," + Environment.NewLine +
            "    {" + Environment.NewLine +
            "        \"Code\": \"KGK\"," + Environment.NewLine +
            "        \"Description\": \"Eigenwicht Bioethanolgehalt in Kilo\"," + Environment.NewLine +
            "        \"UPSCode\": \" \"," + Environment.NewLine +
            "        \"UPSDescription\": \" \"," + Environment.NewLine +
            "        \"CustomsCode\": \"KGK\"," + Environment.NewLine +
            "        \"CustomsDescription\": \"Eigenwicht Bioethanolgehalt in Kilo\"," + Environment.NewLine +
            "        \"OriginalDescription\": \"Eigenwicht Bioethanolgehalt in Kilo\"" + Environment.NewLine +
            "    }," + Environment.NewLine +
            "    {" + Environment.NewLine +
            "        \"Code\": \"KGM\"," + Environment.NewLine +
            "        \"Description\": \"Kilogram\"," + Environment.NewLine +
            "        \"UPSCode\": \" \"," + Environment.NewLine +
            "        \"UPSDescription\": \" \"," + Environment.NewLine +
            "        \"CustomsCode\": \"KGM\"," + Environment.NewLine +
            "        \"CustomsDescription\": \"Kilogram\"," + Environment.NewLine +
            "        \"OriginalDescription\": \"Kilogram\"" + Environment.NewLine +
            "    }," + Environment.NewLine +
            "    {" + Environment.NewLine +
            "        \"Code\": \"KGP\"," + Environment.NewLine +
            "        \"Description\": \"Eigengewicht abgetropft in Kilogram\"," + Environment.NewLine +
            "        \"UPSCode\": \" \"," + Environment.NewLine +
            "        \"UPSDescription\": \" \"," + Environment.NewLine +
            "        \"CustomsCode\": \"KGP\"," + Environment.NewLine +
            "        \"CustomsDescription\": \"Eigengewicht abgetropft in Kilogram\"," + Environment.NewLine +
            "        \"OriginalDescription\": \"Eigengewicht abgetropft in Kilogram\"" + Environment.NewLine +
            "    }," + Environment.NewLine +
            "    {" + Environment.NewLine +
            "        \"Code\": \"KGS\"," + Environment.NewLine +
            "        \"Description\": \"KILOGRAMS\"," + Environment.NewLine +
            "        \"UPSCode\": \"KGS\"," + Environment.NewLine +
            "        \"UPSDescription\": \"KILOGRAMS\"," + Environment.NewLine +
            "        \"CustomsCode\": \" \"," + Environment.NewLine +
            "        \"CustomsDescription\": \" \"," + Environment.NewLine +
            "        \"OriginalDescription\": \"KILOGRAMS\"" + Environment.NewLine +
            "    }," + Environment.NewLine +
            "    {" + Environment.NewLine +
            "        \"Code\": \"KGT\"," + Environment.NewLine +
            "        \"Description\": \"Eigenwicht der Trockenmasse in Kilo\"," + Environment.NewLine +
            "        \"UPSCode\": \" \"," + Environment.NewLine +
            "        \"UPSDescription\": \" \"," + Environment.NewLine +
            "        \"CustomsCode\": \"KGT\"," + Environment.NewLine +
            "        \"CustomsDescription\": \"Eigenwicht der Trockenmasse in Kilo\"," + Environment.NewLine +
            "        \"OriginalDescription\": \"Eigenwicht der Trockenmasse in Kilo\"" + Environment.NewLine +
            "    }," + Environment.NewLine +
            "    {" + Environment.NewLine +
            "        \"Code\": \"KGZ\"," + Environment.NewLine +
            "        \"Description\": \"Kg Rohzuckeräquivalent\"," + Environment.NewLine +
            "        \"UPSCode\": \" \"," + Environment.NewLine +
            "        \"UPSDescription\": \" \"," + Environment.NewLine +
            "        \"CustomsCode\": \"KGZ\"," + Environment.NewLine +
            "        \"CustomsDescription\": \"Kg Rohzuckeräquivalent\"," + Environment.NewLine +
            "        \"OriginalDescription\": \"Kg Rohzuckeräquivalent\"" + Environment.NewLine +
            "    }," + Environment.NewLine +
            "    {" + Environment.NewLine +
            "        \"Code\": \"KHO\"," + Environment.NewLine +
            "        \"Description\": \"Kilogram peroxidu vodíku\"," + Environment.NewLine +
            "        \"UPSCode\": \" \"," + Environment.NewLine +
            "        \"UPSDescription\": \" \"," + Environment.NewLine +
            "        \"CustomsCode\": \"KHO\"," + Environment.NewLine +
            "        \"CustomsDescription\": \"Kilogram peroxidu vodíku\"," + Environment.NewLine +
            "        \"OriginalDescription\": \"Kilogram peroxidu vodíku\"" + Environment.NewLine +
            "    }" + Environment.NewLine +
            "]";
        }

        public static string GetTariffData()
        {
            return
                "[" + Environment.NewLine +
                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"10030200\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"10\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"02\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌（中文+英文）；3.型号1.品名；2.品牌（中文+英文）；3.型号1.品名；2.品牌（中文+英文）；3.型号1.品名；2.品牌（中文+英文）；3.型号1.品名；2.品牌（中文+英文）；3.型号\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"17010121\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"17\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"21\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌(中文+英文)；3.型号 | 相机镜头；佳能CANON；EF 24-105mm IS STM\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"02990000\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"02\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"99\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌(中文+英文)；3.容量\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"19020810\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"19\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"02\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"08\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"10\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌(中文+英文)；3.型号\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"04010300\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"04\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌(中文+英文)\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"04019900\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"04\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"99\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名(类型)；2.品牌(中文+英文)\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"19020411\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"19\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"02\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"04\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"11\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌(中文+英文)；3.型号；4.屏幕尺寸\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"13010200\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"13\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"02\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌(中文+英文)；3.型号；4.功率\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"11032000\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"11\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"20\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌（中文+英文）；3.型号\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"17990300\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"17\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"99\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌(中文+英文)；3.型号\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"14010700\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"14\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"07\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌(中文+英文)；3.型号；4.容积\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"19020911\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"19\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"02\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"09\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"11\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌(中文+英文)；3.型号；4.存储容量\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"09010311\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"09\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"11\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌（中文+英文）；3.规格 | 睫毛膏；兰蔻LANCOME；10g/支\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"22030200\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"22\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"02\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"18990200\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"18\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"99\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"02\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名(类型)；2.品牌(中文+英文)\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"26030000\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"26\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌（中文+英文）；3.型号\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"11011300\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"11\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"13\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌（中文+英文）；3.型号\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"01030200\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"02\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌(中文+英文)；3.容量\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"10019900\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"10\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"99\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名（类型）；2.品牌（中文+英文）；3.型号 | 家用理疗仪；飞利浦PHILIPS；21325\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"19020541\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"19\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"02\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"05\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"41\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌(中文+英文)；3.型号\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"22030220\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"22\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"02\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"20\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名(类型)；2.品牌(中文+英文)\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"04030100\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"04\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名(类型)；2.品牌(中文+英文)；3.规格\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"09010511\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"09\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"05\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"11\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌（中文+英文）；3.规格 | dw持妆粉底液；雅诗兰黛ESTEE LAUDER； 30ml/瓶\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"19010310\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"19\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"10\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌(中文+英文)；3.型号 | 笔记本电脑；荣耀HONOR；KPL-W00\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"01030300\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌(中文+英文)；3.规格；4.成分\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"22030290\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"22\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"02\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"90\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名(类型)；2.品牌(中文+英文)\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"22030110\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"22\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"10\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌(中文+英文)；3.型号\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"05010300\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"05\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌(中文+英文)； 3.材质（牛皮、羊皮、猪皮、人造革等）\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"26090000\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"26\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"09\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名(类型)；2.品牌（中文+英文）；3.型号\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"15030000\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"15\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名(类型)；2.品牌(中文+英文)；3.型号\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"22040200\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"22\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"04\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"02\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名(类型)；2.品牌(中文+英文)\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"07020200\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"07\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"02\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"02\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌（中文+英文）；3.型号\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"14020300\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"14\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"02\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌(中文+英文)；3.型号；4.容量(瓶)\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"17990110\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"17\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"99\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"10\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌(中文+英文)；3.存储容量 | 存储卡；闪迪SANDISK；128GB\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"02020100\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"02\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"02\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌(中文+英文)；3.容量；4.酒精度数；5.产地\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"17029900\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"17\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"02\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"99\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌(中文+英文)；3.型号\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"09010392\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"09\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"92\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名(类型)；2.品牌（中文+英文）；3.规格\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"07010210\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"07\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"02\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"10\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌（中文+英文）；3.型号（款号）； 4.类型（石英、机械等） | 手表；卡西欧CASIO；MQ-24；石英\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"18039900\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"18\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"99\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名(类型)；2.品牌(中文+英文)\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"09010112\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"09\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"12\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌（中文+英文）；3.规格\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"07029900\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"07\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"02\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"99\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名(类型) ；2.品牌（中文+英文）；3.型号\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"09010322\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"09\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"22\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌（中文+英文）；3.规格\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"18010300\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"18\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌(中文+英文)；3.型号 | 收音机；索尼SONY；TFM825\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"18030320\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"18\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"20\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌(中文+英文)\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"04030300\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"04\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌(中文+英文)；3.规格\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"11011600\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"11\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"16\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌（中文+英文）；3.型号\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"15010100\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"15\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"00\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌(中文+英文)；3.型号\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"18030520\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"18\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"05\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"20\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名(类型,CD、DVD、黑胶唱片等）；2.是否录制\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"18030310\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"18\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"03\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"10\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌(中文+英文)\"" + Environment.NewLine +
                "	}," + Environment.NewLine +

                "	{" + Environment.NewLine +
                "		\"HSCODE\": \"09010291\"," + Environment.NewLine +
                "		\"TRF_NR_CHP_CD\": \"09\"," + Environment.NewLine +
                "		\"TRF_NR_SUH_CD\": \"01\"," + Environment.NewLine +
                "		\"TRF_NR_HDR_CD\": \"02\"," + Environment.NewLine +
                "		\"TRF_NR_SBH_CD\": \"91\"," + Environment.NewLine +
                "		\"TRF_NR_TSL_TE1\": \"1.品名；2.品牌（中文+英文）；3.规格\"" + Environment.NewLine +
                "	}," + Environment.NewLine +
            "]";
        }
    }
}
