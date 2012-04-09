using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace aenetmail_csharp
{
    internal static class Utilities
    {
        internal static string ConvertHTML2Text(string source)
        {
            try
            {
                string result;
                result = source;

                // Remove HTML Development formatting
                // Replace line breaks with space
                // because browsers inserts space
                result = result.Replace("\r", " ");
                // Replace line breaks with space
                // because browsers inserts space
                result = result.Replace("\n", " ");
                // Remove step-formatting
                result = result.Replace("\t", string.Empty);
                // Remove repeating spaces because browsers ignore them
                result = System.Text.RegularExpressions.Regex.Replace(result,
                                                                      @"( )+", " ");

                // Remove the header (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*head([^>])*>", "<head>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*head( )*>)", "</head>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(<head>).*(</head>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // remove all scripts (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*script([^>])*>", "<script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*script( )*>)", "</script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                //result = System.Text.RegularExpressions.Regex.Replace(result,
                //         @"(<script>)([^(<script>\.</script>)])*(</script>)",
                //         string.Empty,
                //         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<script>).*(</script>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // remove all styles (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*style([^>])*>", "<style>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*style( )*>)", "</style>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(<style>).*(</style>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert tabs in spaces of <td> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*td([^>])*>", "\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert line breaks in places of <BR> and <LI> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*br( )*>", "\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*li( )*>", "\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert line paragraphs (double line breaks) in place
                // if <P>, <DIV> and <TR> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*div([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*tr([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*p([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // Remove remaining tags like <a>, links, images,
                // comments etc - anything that's enclosed inside < >
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<[^>]*>", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // replace special characters:
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @" ", " ",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&bull;", " * ",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&lsaquo;", "<",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&rsaquo;", ">",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&trade;", "(tm)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&frasl;", "/",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&lt;", "<",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&gt;", ">",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&copy;", "(c)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&reg;", "(r)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove all others. More can be added, see
                result = Replace_SpecialChars(result);

                // for testing
                //System.Text.RegularExpressions.Regex.Replace(result,
                //       this.txtRegex.Text,string.Empty,
                //       System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // make line breaking consistent
                result = result.Replace("\n", "\r");

                // Remove extra line breaks and tabs:
                // replace over 2 breaks with 2 and over 4 tabs with 4.
                // Prepare first to remove any whitespaces in between
                // the escaped characters and remove redundant tabs in between line breaks
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)( )+(\r)", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\t)( )+(\t)", "\t\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\t)( )+(\r)", "\t\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)( )+(\t)", "\r\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove redundant tabs
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)(\t)+(\r)", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove multiple tabs following a line break with just one tab
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)(\t)+", "\r\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Initial replacement target string for line breaks
                string breaks = "\r\r\r";
                // Initial replacement target string for tabs
                string tabs = "\t\t\t\t\t";
                for (int index = 0; index < result.Length; index++)
                {
                    result = result.Replace(breaks, "\r\r");
                    result = result.Replace(tabs, "\t\t\t\t");
                    breaks = breaks + "\r";
                    tabs = tabs + "\t";
                }

                //remove white-space characters
                result = result.Trim();

                // That's it.
                return result;
            }
            catch
            {
                return source;
            }
        }
        internal static void TryDispose<T>(ref T obj) where T : class, IDisposable
        {
            try
            {
                if (obj != null)
                    obj.Dispose();
            }
            catch (Exception) { }
            obj = null;
        }
        internal static string NotEmpty(this string input, params string[] others)
        {
            if (!string.IsNullOrEmpty(input))
                return input;
            foreach (var item in others)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    return item;
                }
            }
            return string.Empty;
        }
        internal static int ToInt(this string input)
        {
            int result;
            if (int.TryParse(input, out result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }
        internal static DateTime? ToNullDate(this string input)
        {
            DateTime result;
            input = NormalizeDate(input);
            if (DateTime.TryParse(input, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        private static string Replace_SpecialChars(string input)
        {
            string result = input;
            result = result.Replace("&#65;", "A");
            result = result.Replace("&#97;", "a");
            result = result.Replace("&#192;", "À");
            result = result.Replace("&#224;", "à");
            result = result.Replace("&#193;", "Á");
            result = result.Replace("&#225;", "á");
            result = result.Replace("&#194;", "Â");
            result = result.Replace("&#226;", "â");
            result = result.Replace("&#195;", "Ã");
            result = result.Replace("&#227;", "ã");
            result = result.Replace("&#196;", "Ä");
            result = result.Replace("&#228;", "ä");
            result = result.Replace("&#197;", "Å");
            result = result.Replace("&#229;", "å");
            result = result.Replace("&#256;", "Ā");
            result = result.Replace("&#257;", "ā");
            result = result.Replace("&#258;", "Ă");
            result = result.Replace("&#259;", "ă");
            result = result.Replace("&#260;", "Ą");
            result = result.Replace("&#261;", "ą");
            result = result.Replace("&#478;", "Ǟ");
            result = result.Replace("&#479;", "ǟ");
            result = result.Replace("&#506;", "Ǻ");
            result = result.Replace("&#507;", "ǻ");
            result = result.Replace("&#198;", "Æ");
            result = result.Replace("&#230;", "æ");
            result = result.Replace("&#508;", "Ǽ");
            result = result.Replace("&#509;", "ǽ");
            result = result.Replace("&#66;", "B");
            result = result.Replace("&#98;", "b");
            result = result.Replace("&#7682;", "Ḃ");
            result = result.Replace("&#7683;", "ḃ");
            result = result.Replace("&#67;", "C");
            result = result.Replace("&#99;", "c");
            result = result.Replace("&#262;", "Ć");
            result = result.Replace("&#263;", "ć");
            result = result.Replace("&#199;", "Ç");
            result = result.Replace("&#231;", "ç");
            result = result.Replace("&#268;", "Č");
            result = result.Replace("&#269;", "č");
            result = result.Replace("&#264;", "Ĉ");
            result = result.Replace("&#265;", "ĉ");
            result = result.Replace("&#266;", "Ċ");
            result = result.Replace("&#267;", "ċ");
            result = result.Replace("&#68;", "D");
            result = result.Replace("&#100;", "d");
            result = result.Replace("&#7696;", "Ḑ");
            result = result.Replace("&#7697;", "ḑ");
            result = result.Replace("&#270;", "Ď");
            result = result.Replace("&#271;", "ď");
            result = result.Replace("&#7690;", "Ḋ");
            result = result.Replace("&#7691;", "ḋ");
            result = result.Replace("&#272;", "Đ");
            result = result.Replace("&#273;", "đ");
            result = result.Replace("&#208;", "Ð");
            result = result.Replace("&#240;", "ð");
            result = result.Replace("&498;", "Dz");
            result = result.Replace("&#497;", "DZ");
            result = result.Replace("&#499;", "ǳ");
            result = result.Replace("&#452;", "Ǆ");
            result = result.Replace("&#453;", "ǅ");
            result = result.Replace("&#454;", "ǆ");
            result = result.Replace("&#69;", "E");
            result = result.Replace("&#101;", "e");
            result = result.Replace("&#200;", "È");
            result = result.Replace("&#232;", "è");
            result = result.Replace("&#201;", "É");
            result = result.Replace("&#233;", "é");
            result = result.Replace("&#282;", "Ě");
            result = result.Replace("&#283;", "ě");
            result = result.Replace("&#202;", "Ê");
            result = result.Replace("&#234;", "ê");
            result = result.Replace("&#203;", "Ë");
            result = result.Replace("&#235;", "ë");
            result = result.Replace("&#274;", "Ē");
            result = result.Replace("&#275;", "ē");
            result = result.Replace("&#276;", "Ĕ");
            result = result.Replace("&#277;", "ĕ");
            result = result.Replace("&#280;", "Ę");
            result = result.Replace("&#281;", "ę");
            result = result.Replace("&#278;", "Ė");
            result = result.Replace("&#279;", "ė");
            result = result.Replace("&#439;", "Ʒ");
            result = result.Replace("&#658;", "ʒ");
            result = result.Replace("&#494;", "Ǯ");
            result = result.Replace("&#495;", "ǯ");
            result = result.Replace("&#70;", "F");
            result = result.Replace("&#102;", "f");
            result = result.Replace("&#7710;", "Ḟ");
            result = result.Replace("&#7711;", "ḟ");
            result = result.Replace("&#402;", "ƒ");
            result = result.Replace("&#64256;", "ﬀ");
            result = result.Replace("&#64257;", "ﬁ");
            result = result.Replace("&#64258;", "ﬂ");
            result = result.Replace("&#64259;", "ﬃ");
            result = result.Replace("&#64260;", "ﬄ");
            result = result.Replace("&#64261;", "ﬅ");
            result = result.Replace("&#71;", "G");
            result = result.Replace("&#103;", "g");
            result = result.Replace("&#500;", "Ǵ");
            result = result.Replace("&#501;", "ǵ");
            result = result.Replace("&#290;", "Ģ");
            result = result.Replace("&#291;", "ģ");
            result = result.Replace("&#486;", "Ǧ");
            result = result.Replace("&#487;", "ǧ");
            result = result.Replace("&#284;", "Ĝ");
            result = result.Replace("&#285;", "ĝ");
            result = result.Replace("&#286;", "Ğ");
            result = result.Replace("&#287;", "ğ");
            result = result.Replace("&#288;", "Ġ");
            result = result.Replace("&#289;", "ġ");
            result = result.Replace("&#484;", "Ǥ");
            result = result.Replace("&#485;", "ǥ");
            result = result.Replace("&#72;", "H");
            result = result.Replace("&#104;", "h");
            result = result.Replace("&#292;", "Ĥ");
            result = result.Replace("&#293;", "ĥ");
            result = result.Replace("&#294;", "Ħ");
            result = result.Replace("&#295;", "ħ");
            result = result.Replace("&#73;", "I");
            result = result.Replace("&#105;", "i");
            result = result.Replace("&#204;", "Ì");
            result = result.Replace("&#236;", "ì");
            result = result.Replace("&#205;", "Í");
            result = result.Replace("&#237;", "í");
            result = result.Replace("&#206;", "Î");
            result = result.Replace("&#238;", "î");
            result = result.Replace("&#296;", "Ĩ");
            result = result.Replace("&#297;", "ĩ");
            result = result.Replace("&#207;", "Ï");
            result = result.Replace("&#239;", "ï");
            result = result.Replace("&#298;", "Ī");
            result = result.Replace("&#299;", "ī");
            result = result.Replace("&#300;", "Ĭ");
            result = result.Replace("&#301;", "ĭ");
            result = result.Replace("&#302;", "Į");
            result = result.Replace("&#303;", "į");
            result = result.Replace("&#304;", "İ");
            result = result.Replace("&#305;", "ı");
            result = result.Replace("&#306;", "Ĳ");
            result = result.Replace("&#307;", "ĳ");
            result = result.Replace("&#74;", "J");
            result = result.Replace("&#106;", "j");
            result = result.Replace("&#308;", "Ĵ");
            result = result.Replace("&#309;", "ĵ");
            result = result.Replace("&#75;", "K");
            result = result.Replace("&#107;", "k");
            result = result.Replace("&#7728;", "Ḱ");
            result = result.Replace("&#7729;", "ḱ");
            result = result.Replace("&#310;", "Ķ");
            result = result.Replace("&#311;", "ķ");
            result = result.Replace("&#488;", "Ǩ");
            result = result.Replace("&#489;", "ǩ");
            result = result.Replace("&#312;", "ĸ");
            result = result.Replace("&#76;", "L");
            result = result.Replace("&#108;", "l");
            result = result.Replace("&#313;", "Ĺ");
            result = result.Replace("&#314;", "ĺ");
            result = result.Replace("&#315;", "Ļ");
            result = result.Replace("&#316;", "ļ");
            result = result.Replace("&#317;", "Ľ");
            result = result.Replace("&#318;", "ľ");
            result = result.Replace("&#319;", "Ŀ");
            result = result.Replace("&#320;", "ŀ");
            result = result.Replace("&#321;", "Ł");
            result = result.Replace("&#322;", "ł");
            result = result.Replace("&#455;", "Ǉ");
            result = result.Replace("&#456;", "ǈ");
            result = result.Replace("&#457;", "ǉ");
            result = result.Replace("&#77;", "M");
            result = result.Replace("&#109;", "m");
            result = result.Replace("&#7744;", "Ṁ");
            result = result.Replace("&#7745;", "ṁ");
            result = result.Replace("&#78;", "N");
            result = result.Replace("&#110;", "n");
            result = result.Replace("&#323;", "Ń");
            result = result.Replace("&#324;", "ń");
            result = result.Replace("&#325;", "Ņ");
            result = result.Replace("&#326;", "ņ");
            result = result.Replace("&#327;", "Ň");
            result = result.Replace("&#328;", "ň");
            result = result.Replace("&#209;", "Ñ");
            result = result.Replace("&#241;", "ñ");
            result = result.Replace("&#329;", "ŉ");
            result = result.Replace("&#330;", "Ŋ");
            result = result.Replace("&#331;", "ŋ");
            result = result.Replace("&#458;", "Ǌ");
            result = result.Replace("&#459;", "ǋ");
            result = result.Replace("&#460;", "ǌ");
            result = result.Replace("&#79;", "O");
            result = result.Replace("&#111;", "o");
            result = result.Replace("&#210;", "Ò");
            result = result.Replace("&#242;", "ò");
            result = result.Replace("&#211;", "Ó");
            result = result.Replace("&#243;", "ó");
            result = result.Replace("&#212;", "Ô");
            result = result.Replace("&#244;", "ô");
            result = result.Replace("&#213;", "Õ");
            result = result.Replace("&#245;", "õ");
            result = result.Replace("&#214;", "Ö");
            result = result.Replace("&#246;", "ö");
            result = result.Replace("&#332;", "Ō");
            result = result.Replace("&#333;", "ō");
            result = result.Replace("&#334;", "Ŏ");
            result = result.Replace("&#335;", "ŏ");
            result = result.Replace("&#216;", "Ø");
            result = result.Replace("&#248;", "ø");
            result = result.Replace("&#336;", "Ő");
            result = result.Replace("&#337;", "ő");
            result = result.Replace("&#510;", "Ǿ");
            result = result.Replace("&#511;", "ǿ");
            result = result.Replace("&#338;", "Œ");
            result = result.Replace("&#339;", "œ");
            result = result.Replace("&#80;", "P");
            result = result.Replace("&#112;", "p");
            result = result.Replace("&#7766;", "Ṗ");
            result = result.Replace("&#7767;", "ṗ");
            result = result.Replace("&#81;", "Q");
            result = result.Replace("&#113;", "q");
            result = result.Replace("&#82;", "R");
            result = result.Replace("&#114;", "r");
            result = result.Replace("&#340;", "Ŕ");
            result = result.Replace("&#341;", "ŕ");
            result = result.Replace("&#342;", "Ŗ");
            result = result.Replace("&#343;", "ŗ");
            result = result.Replace("&#344;", "Ř");
            result = result.Replace("&#345;", "ř");
            result = result.Replace("&#636;", "ɼ");
            result = result.Replace("&#83;", "S");
            result = result.Replace("&#115;", "s");
            result = result.Replace("&#346;", "Ś");
            result = result.Replace("&#347;", "ś");
            result = result.Replace("&#350;", "Ş");
            result = result.Replace("&#351;", "ş");
            result = result.Replace("&#352;", "Š");
            result = result.Replace("&#353;", "š");
            result = result.Replace("&#348;", "Ŝ");
            result = result.Replace("&#349;", "ŝ");
            result = result.Replace("&#7776;", "Ṡ");
            result = result.Replace("&#7777;", "ṡ");
            result = result.Replace("&#383;", "ſ");
            result = result.Replace("&#223;", "ß");
            result = result.Replace("&#84;", "T");
            result = result.Replace("&#116;", "t");
            result = result.Replace("&#354;", "Ţ");
            result = result.Replace("&#355;", "ţ");
            result = result.Replace("&#356;", "Ť");
            result = result.Replace("&#357;", "ť");
            result = result.Replace("&#7786;", "Ṫ");
            result = result.Replace("&#7787;", "ṫ");
            result = result.Replace("&#358;", "Ŧ");
            result = result.Replace("&#359;", "ŧ");
            result = result.Replace("&#222;", "Þ");
            result = result.Replace("&#254;", "þ");
            result = result.Replace("&#85;", "U");
            result = result.Replace("&#117;", "u");
            result = result.Replace("&#217;", "Ù");
            result = result.Replace("&#249;", "ù");
            result = result.Replace("&#218;", "Ú");
            result = result.Replace("&#250;", "ú");
            result = result.Replace("&#219;", "Û");
            result = result.Replace("&#251;", "û");
            result = result.Replace("&#360;", "Ũ");
            result = result.Replace("&#361;", "ũ");
            result = result.Replace("&#220;", "Ü");
            result = result.Replace("&#252;", "ü");
            result = result.Replace("&#366;", "Ů");
            result = result.Replace("&#367;", "ů");
            result = result.Replace("&#362;", "Ū");
            result = result.Replace("&#363;", "ū");
            result = result.Replace("&#364;", "Ŭ");
            result = result.Replace("&#365;", "ŭ");
            result = result.Replace("&#370;", "Ų");
            result = result.Replace("&#371;", "ų");
            result = result.Replace("&#368;", "Ű");
            result = result.Replace("&#369;", "ű");
            result = result.Replace("&#86;", "V");
            result = result.Replace("&#118;", "v");
            result = result.Replace("&#87;", "W");
            result = result.Replace("&#119;", "w");
            result = result.Replace("&#7808;", "Ẁ");
            result = result.Replace("&#7809;", "ẁ");
            result = result.Replace("&#7810;", "Ẃ");
            result = result.Replace("&#7811;", "ẃ");
            result = result.Replace("&#372;", "Ŵ");
            result = result.Replace("&#373;", "ŵ");
            result = result.Replace("&#7812;", "Ẅ");
            result = result.Replace("&#7813;", "ẅ");
            result = result.Replace("&#88;", "X");
            result = result.Replace("&#120;", "x");
            result = result.Replace("&#89;", "Y");
            result = result.Replace("&#121;", "y");
            result = result.Replace("&#7922;", "Ỳ");
            result = result.Replace("&#7923;", "ỳ");
            result = result.Replace("&#221;", "Ý");
            result = result.Replace("&#253;", "ý");
            result = result.Replace("&#374;", "Ŷ");
            result = result.Replace("&#375;", "ŷ");
            result = result.Replace("&#159;", "Ÿ");
            result = result.Replace("&#255;", "ÿ");
            result = result.Replace("&#90;", "Z");
            result = result.Replace("&#122;", "z");
            result = result.Replace("&#377;", "Ź");
            result = result.Replace("&#378;", "ź");
            result = result.Replace("&#381;", "Ž");
            result = result.Replace("&#382;", "ž");
            result = result.Replace("&#379;", "Ż");
            result = result.Replace("&#380;", "ż");
            result = result.Replace("A", "A");
            result = result.Replace("a", "a");
            result = result.Replace("&Agrave;", "À");
            result = result.Replace("&agrave;", "à");
            result = result.Replace("&Aacute;", "Á");
            result = result.Replace("&aacute;", "á");
            result = result.Replace("&Acirc;", "Â");
            result = result.Replace("&acirc;", "â");
            result = result.Replace("&Atilde;", "Ã");
            result = result.Replace("&atilde;", "ã");
            result = result.Replace("&Auml;", "Ä");
            result = result.Replace("&auml;", "ä");
            result = result.Replace("&Aring;", "Å");
            result = result.Replace("&aring;", "å");
            result = result.Replace("&AElig;", "Æ");
            result = result.Replace("&aelig;", "æ");
            result = result.Replace("B", "B");
            result = result.Replace("b", "b");
            result = result.Replace("C", "C");
            result = result.Replace("c", "c");
            result = result.Replace("&Ccedil;", "Ç");
            result = result.Replace("&ccedil;", "ç");
            result = result.Replace("D", "D");
            result = result.Replace("d", "d");
            result = result.Replace("&ETH;", "Ð");
            result = result.Replace("&eth;", "ð");
            result = result.Replace("E", "E");
            result = result.Replace("e", "e");
            result = result.Replace("&Egrave;", "È");
            result = result.Replace("&egrave;", "è");
            result = result.Replace("&Eacute;", "É");
            result = result.Replace("&eacute;", "é");
            result = result.Replace("&Ecirc;", "Ê");
            result = result.Replace("&ecirc;", "ê");
            result = result.Replace("&Euml;", "Ë");
            result = result.Replace("&euml;", "ë");
            result = result.Replace("F", "F");
            result = result.Replace("f", "f");
            result = result.Replace("G", "G");
            result = result.Replace("g", "g");
            result = result.Replace("H", "H");
            result = result.Replace("h", "h");
            result = result.Replace("I", "I");
            result = result.Replace("i", "i");
            result = result.Replace("&Igrave;", "Ì");
            result = result.Replace("&igrave;", "ì");
            result = result.Replace("&Iacute;", "Í");
            result = result.Replace("&iacute;", "í");
            result = result.Replace("&Icirc;", "Î");
            result = result.Replace("&icirc;", "î");
            result = result.Replace("&Iuml;", "Ï");
            result = result.Replace("&iuml;", "ï");
            result = result.Replace("J", "J");
            result = result.Replace("j", "j");
            result = result.Replace("K", "K");
            result = result.Replace("k", "k");
            result = result.Replace("L", "L");
            result = result.Replace("l", "l");
            result = result.Replace("M", "M");
            result = result.Replace("m", "m");
            result = result.Replace("N", "N");
            result = result.Replace("n", "n");
            result = result.Replace("&Ntilde;", "Ñ");
            result = result.Replace("&ntilde;", "ñ");
            result = result.Replace("O", "O");
            result = result.Replace("o", "o");
            result = result.Replace("&Ograve;", "Ò");
            result = result.Replace("&ograve;", "ò");
            result = result.Replace("&Oacute;", "Ó");
            result = result.Replace("&oacute;", "ó");
            result = result.Replace("&Ocirc;", "Ô");
            result = result.Replace("&ocirc;", "ô");
            result = result.Replace("&Otilde;", "Õ");
            result = result.Replace("&otilde;", "õ");
            result = result.Replace("&Ouml;", "Ö");
            result = result.Replace("&ouml;", "ö");
            result = result.Replace("&Oslash;", "Ø");
            result = result.Replace("&oslash;", "ø");
            result = result.Replace("&OElig;", "Œ");
            result = result.Replace("&oelig;", "œ");
            result = result.Replace("P", "P");
            result = result.Replace("p", "p");
            result = result.Replace("Q", "Q");
            result = result.Replace("q", "q");
            result = result.Replace("R", "R");
            result = result.Replace("r", "r");
            result = result.Replace("S", "S");
            result = result.Replace("s", "s");
            result = result.Replace("&szlig;", "ß");
            result = result.Replace("T", "T");
            result = result.Replace("t", "t");
            result = result.Replace("&THORN;", "Þ");
            result = result.Replace("&thorn;", "þ");
            result = result.Replace("U", "U");
            result = result.Replace("u", "u");
            result = result.Replace("&Ugrave;", "Ù");
            result = result.Replace("&ugrave;", "ù");
            result = result.Replace("&Uacute;", "Ú");
            result = result.Replace("&uacute;", "ú");
            result = result.Replace("&Ucirc;", "Û");
            result = result.Replace("&ucirc;", "û");
            result = result.Replace("&Uuml;", "Ü");
            result = result.Replace("&uuml;", "ü");
            result = result.Replace("V", "V");
            result = result.Replace("v", "v");
            result = result.Replace("W", "W");
            result = result.Replace("w", "w");
            result = result.Replace("X", "X");
            result = result.Replace("x", "x");
            result = result.Replace("Y", "Y");
            result = result.Replace("y", "y");
            result = result.Replace("&Yacute;", "Ý");
            result = result.Replace("&yacute;", "ý");
            result = result.Replace("&Yuml;", "Ÿ");
            result = result.Replace("&yuml;", "ÿ");
            result = result.Replace("Z", "Z");
            result = result.Replace("z", "z");
            result = result.Replace("&nbsp;", " ");
            return result;
        }
        private static Regex rxTimeZoneName = new Regex(@"\s+\([a-z]+\)$", RegexOptions.Compiled | RegexOptions.IgnoreCase); //Mon, 28 Feb 2005 19:26:34 -0500 (EST)
        private static Regex rxTimeZoneColon = new Regex(@"\s+(\+|\-)(\d{1,2})\D(\d{2})$", RegexOptions.Compiled | RegexOptions.IgnoreCase); //Mon, 28 Feb 2005 19:26:34 -0500 (EST)
        private static Regex rxTimeZoneMinutes = new Regex(@"([\+\-]?\d{1,2})(\d{2})$", RegexOptions.Compiled); //search can be strict because the format has already been normalized
        private static Regex rxNegativeHours = new Regex(@"(?<=\s)\-(?=\d{1,2}\:)", RegexOptions.Compiled);

        internal static string NormalizeDate(string value)
        {
            value = rxTimeZoneName.Replace(value, string.Empty);
            value = rxTimeZoneColon.Replace(value, match => " " + match.Groups[1].Value + match.Groups[2].Value.PadLeft(2, '0') + match.Groups[3].Value);
            value = rxNegativeHours.Replace(value, string.Empty);
            var minutes = rxTimeZoneMinutes.Match(value);
            if (minutes.Groups[2].Value.ToInt() > 60)
            { //even if there's no match, the value = 0
                value = value.Substring(0, minutes.Index) + minutes.Groups[1].Value + "00";
            }
            return value;
        }
        internal static string GetRFC2060Date(this DateTime date)
        {
            CultureInfo enUsCulture = CultureInfo.GetCultureInfo("en-US");
            return date.ToString("dd-MMM-yyyy hh:mm:ss zz", enUsCulture);
        }
        internal static string QuoteString(this string value)
        {
            return "\"" + value
                            .Replace("\\", "\\\\")
                            .Replace("\r", "\\r")
                            .Replace("\n", "\\n")
                            .Replace("\"", "\\\"") + "\"";
        }
        internal static bool StartsWithWhiteSpace(this string line)
        {
            if (string.IsNullOrEmpty(line))
                return false;
            var chr = line[0];
            return chr == ' ' || chr == '\t' || chr == '\n' || chr == '\r';
        }
        internal static string DecodeQuotedPrintable(string value, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = System.Text.Encoding.Default;
            }

            if (value.IndexOf('_') > -1 && value.IndexOf(' ') == -1)
                value = value.Replace('_', ' ');

            var data = System.Text.Encoding.ASCII.GetBytes(value);
            var eq = Convert.ToByte('=');
            var n = 0;
            for (int i = 0; i < data.Length; i++)
            {
                var b = data[i];

                if (b == eq)
                {
                    byte b1 = data[i + 1], b2 = data[i + 2];
                    if (b1 == 10 || b1 == 13)
                    {
                        i++;
                        if (b2 == 10 || b2 == 13)
                        {
                            i++;
                        }
                        continue;
                    }

                    data[n] = (byte)int.Parse(value.Substring(i + 1, 2), NumberStyles.HexNumber);
                    n++;
                    i += 2;

                }
                else
                {
                    data[n] = b;
                    n++;
                }
            }

            value = encoding.GetString(data, 0, n);
            return value;
        }
        internal static string DecodeBase64(string data, Encoding encoding = null)
        {
            if (!IsValidBase64String(data))
            {
                return data;
            }
            var bytes = Convert.FromBase64String(data);
            return (encoding ?? System.Text.Encoding.Default).GetString(bytes);
        }

        #region OpenPOP.NET
        internal static string DecodeWords(string encodedWords, Encoding @default = null)
        {
            if (string.IsNullOrEmpty(encodedWords))
                return string.Empty;

            string decodedWords = encodedWords;

            // Notice that RFC2231 redefines the BNF to
            // encoded-word := "=?" charset ["*" language] "?" encoded-text "?="
            // but no usage of this BNF have been spotted yet. It is here to
            // ease debugging if such a case is discovered.

            // This is the regex that should fit the BNF
            // RFC Says that NO WHITESPACE is allowed in this encoding, but there are examples
            // where whitespace is there, and therefore this regex allows for such.
            const string strRegEx = @"\=\?(?<Charset>\S+?)\?(?<Encoding>\w)\?(?<Content>.+?)\?\=";
            // \w	Matches any word character including underscore. Equivalent to "[A-Za-z0-9_]".
            // \S	Matches any nonwhite space character. Equivalent to "[^ \f\n\r\t\v]".
            // +?   non-gready equivalent to +
            // (?<NAME>REGEX) is a named group with name NAME and regular expression REGEX

            var matches = Regex.Matches(encodedWords, strRegEx);
            foreach (Match match in matches)
            {
                // If this match was not a success, we should not use it
                if (!match.Success)
                    continue;

                string fullMatchValue = match.Value;

                string encodedText = match.Groups["Content"].Value;
                string encoding = match.Groups["Encoding"].Value;
                string charset = match.Groups["Charset"].Value;

                // Get the encoding which corrosponds to the character set
                Encoding charsetEncoding = ParseCharsetToEncoding(charset, @default);

                // Store decoded text here when done
                string decodedText;

                // Encoding may also be written in lowercase
                switch (encoding.ToUpperInvariant())
                {
                    // RFC:
                    // The "B" encoding is identical to the "BASE64" 
                    // encoding defined by RFC 2045.
                    // http://tools.ietf.org/html/rfc2045#section-6.8
                    case "B":
                        decodedText = DecodeBase64(encodedText, charsetEncoding);
                        break;

                    // RFC:
                    // The "Q" encoding is similar to the "Quoted-Printable" content-
                    // transfer-encoding defined in RFC 2045.
                    // There are more details to this. Please check
                    // http://tools.ietf.org/html/rfc2047#section-4.2
                    // 
                    case "Q":
                        decodedText = DecodeQuotedPrintable(encodedText, charsetEncoding);
                        break;

                    default:
                        throw new ArgumentException("The encoding " + encoding + " was not recognized");
                }

                // Repalce our encoded value with our decoded value
                decodedWords = decodedWords.Replace(fullMatchValue, decodedText);
            }

            return decodedWords;
        }

        //http://www.opensourcejavaphp.net/csharp/openpopdotnet/HeaderFieldParser.cs.html
        /// Parse a character set into an encoding.
        /// </summary>
        /// <param name="characterSet">The character set to parse</param>
        /// <returns>An encoding which corresponds to the character set</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="characterSet"/> is <see langword="null"/></exception>
        public static Encoding ParseCharsetToEncoding(string characterSet, Encoding @default)
        {
            if (string.IsNullOrEmpty(characterSet))
                return @default ?? Encoding.Default;

            string charSetUpper = characterSet.ToUpperInvariant();
            if (charSetUpper.Contains("WINDOWS") || charSetUpper.Contains("CP"))
            {
                // It seems the character set contains an codepage value, which we should use to parse the encoding
                charSetUpper = charSetUpper.Replace("CP", ""); // Remove cp
                charSetUpper = charSetUpper.Replace("WINDOWS", ""); // Remove windows
                charSetUpper = charSetUpper.Replace("-", ""); // Remove - which could be used as cp-1554

                // Now we hope the only thing left in the characterSet is numbers.
                int codepageNumber = int.Parse(charSetUpper, System.Globalization.CultureInfo.InvariantCulture);

                return Encoding.GetEncodings().Where(x => x.CodePage == codepageNumber)
                  .Select(x => x.GetEncoding()).FirstOrDefault() ?? @default ?? Encoding.Default;
            }

            // It seems there is no codepage value in the characterSet. It must be a named encoding
            return Encoding.GetEncodings().Where(x => x.Name.Is(characterSet))
              .Select(x => x.GetEncoding()).FirstOrDefault() ?? @default ?? System.Text.Encoding.Default;
        }
        #endregion


        #region IsValidBase64
        //stolen from http://stackoverflow.com/questions/3355407/validate-string-is-base64-format-using-regex
        private const char Base64Padding = '=';

        private static readonly HashSet<char> Base64Characters = new HashSet<char>() { 
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 
            'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 
            'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 
            'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/'
        };

        internal static bool IsValidBase64String(string param)
        {
            if (param == null)
            {
                // null string is not Base64 
                return false;
            }

            // replace optional CR and LF characters
            param = param.Replace("\r", String.Empty).Replace("\n", String.Empty);

            int lengthWPadding = param.Length;
            if (lengthWPadding == 0 || (lengthWPadding % 4) != 0)
            {
                // Base64 string should not be empty
                // Base64 string length should be multiple of 4
                return false;
            }

            // replace pad chacters
            int lengthWOPadding;

            param = param.TrimEnd(Base64Padding);
            lengthWOPadding = param.Length;

            if ((lengthWPadding - lengthWOPadding) > 2)
            {
                // there should be no more than 2 pad characters
                return false;
            }

            foreach (char c in param)
            {
                if (!Base64Characters.Contains(c))
                {
                    // string contains non-Base64 character
                    return false;
                }
            }

            // nothing invalid found
            return true;
        }
        #endregion

        internal static VT Get<KT, VT>(this IDictionary<KT, VT> dictionary, KT key, VT defaultValue = default(VT))
        {
            if (dictionary == null)
                return defaultValue;
            VT value;
            if (dictionary.TryGetValue(key, out value))
                return value;
            return defaultValue;
        }
        internal static void Set<KT, VT>(this IDictionary<KT, VT> dictionary, KT key, VT value)
        {
            if (!dictionary.ContainsKey(key))
                lock (dictionary)
                    if (!dictionary.ContainsKey(key))
                    {
                        dictionary.Add(key, value);
                        return;
                    }

            dictionary[key] = value;
        }
        internal static void Fire<T>(this EventHandler<T> events, object sender, T args) where T : EventArgs
        {
            if (events == null)
                return;
            events(sender, args);
        }
        internal static MailAddress ToEmailAddress(this string input)
        {
            try
            {
                return new MailAddress(input);
            }
            catch (Exception)
            {
                return null;
            }
        }
        internal static bool Is(this string input, string other)
        {
            return string.Equals(input, other, StringComparison.OrdinalIgnoreCase);
        }
    }
}