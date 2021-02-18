using System.Text.RegularExpressions;

namespace USAA_Gen2Library
{
    public class Authentication
    {
        public string Version = "2";
        public string EncPrimitives = "RSA-688";
        public string SignPrimitives = "RSA-688";
        public string MACPrimitives = "SHA256";
        public string MasterKeyHash = "b3dac0e22f65011ebae745ccc6b76838";

        public string Type { set; get; } = string.Empty;

        public string Target { set; get; } = string.Empty;

        public string MainBody { set; get; } = string.Empty;

        public bool IsSubsidiaryBodyLocked { set; get; }

        public string SubsidiaryBodies { set; get; } = string.Empty;

        public string TimeStart { set; get; } = string.Empty;

        public int TimeSustain { set; get; }

        public string ExpressService { set; get; } = "Normal";

        public string NyarlathotepService { set; get; } = "Normal";

        public string NotesService { set; get; } = "Normal";

        public string ProsService { set; get; } = "Normal";

        public string InfonetService { set; get; } = "Normal";

        public bool IsCoreUser { set; get; }

        public bool IsPayingUser { set; get; }

        public int AdvancedSupport { set; get; }

        public bool IsOverseasService { set; get; }

        public string Region { set; get; } = "Mainland, China";

        public string Comments { set; get; } = string.Empty;

        public string HeaderXML { set; get; } = string.Empty;

        public string MessageXML { set; get; } = string.Empty;

        public bool Encoding(ref string ParsedXML)
        {
            if (this.Type == string.Empty || this.Target == string.Empty || (this.TimeStart == string.Empty || this.TimeSustain <= 0))
                return false;
            if (this.MainBody == string.Empty)
                this.MainBody = this.Target;
            if (this.SubsidiaryBodies == string.Empty)
                this.IsSubsidiaryBodyLocked = false;
            if (this.Region == string.Empty)
                this.IsOverseasService = false;
            this.HeaderXML += string.Format("<Version>{0}</Version>", (object)this.Version);
            this.HeaderXML += string.Format("<EncPrimitives>{0}</EncPrimitives>", (object)this.EncPrimitives);
            this.HeaderXML += string.Format("<SignPrimitives>{0}</SignPrimitives>", (object)this.SignPrimitives);
            this.HeaderXML += string.Format("<MACPrimitives>{0}</MACPrimitives>", (object)this.MACPrimitives);
            this.HeaderXML += string.Format("<MasterKeyHash>{0}</MasterKeyHash>", (object)this.MasterKeyHash);
            ParsedXML = string.Empty;
            ParsedXML += string.Format("<Version>{0}</Version>", (object)this.Version);
            ParsedXML += string.Format("<EncPrimitives>{0}</EncPrimitives>", (object)this.EncPrimitives);
            ParsedXML += string.Format("<SignPrimitives>{0}</SignPrimitives>", (object)this.SignPrimitives);
            ParsedXML += string.Format("<MACPrimitives>{0}</MACPrimitives>", (object)this.MACPrimitives);
            ParsedXML += string.Format("<MasterKeyHash>{0}</MasterKeyHash>", (object)this.MasterKeyHash);
            ParsedXML += string.Format("<Type>{0}</Type>", (object)this.Type);
            ParsedXML += string.Format("<Target>{0}</Target>", (object)this.Target);
            ParsedXML += string.Format("<MainBody>{0}</MainBody>", (object)this.MainBody);
            ParsedXML += string.Format("<IsSubsidiaryBodyLocked>{0}</IsSubsidiaryBodyLocked>", (object)this.IsSubsidiaryBodyLocked.ToString());
            ParsedXML += string.Format("<SubsidiaryBodies>{0}</SubsidiaryBodies>", (object)this.SubsidiaryBodies);
            ParsedXML += string.Format("<TimeStart>{0}</TimeStart>", (object)this.TimeStart);
            ParsedXML += string.Format("<TimeSustain>{0}</TimeSustain>", (object)this.TimeSustain.ToString());
            ParsedXML += string.Format("<ExpressService>{0}</ExpressService>", (object)this.ExpressService);
            ParsedXML += string.Format("<NyarlathotepService>{0}</NyarlathotepService>", (object)this.NyarlathotepService);
            ParsedXML += string.Format("<NotesService>{0}</NotesService>", (object)this.NotesService);
            ParsedXML += string.Format("<ProsService>{0}</ProsService>", (object)this.ProsService);
            ParsedXML += string.Format("<InfonetService>{0}</InfonetService>", (object)this.InfonetService);
            ParsedXML += string.Format("<IsCoreUser>{0}</IsCoreUser>", (object)this.IsCoreUser.ToString());
            ParsedXML += string.Format("<IsPayingUser>{0}</IsPayingUser>", (object)this.IsPayingUser.ToString());
            ParsedXML += string.Format("<AdvancedSupport>{0}</AdvancedSupport>", (object)this.AdvancedSupport.ToString());
            ParsedXML += string.Format("<IsOverseasService>{0}</IsOverseasService>", (object)this.IsOverseasService);
            ParsedXML += string.Format("<Region>{0}</Region>", (object)this.Region);
            ParsedXML += string.Format("<Comments>{0}</Comments>", (object)this.Comments);
            this.MessageXML = ParsedXML;
            return true;
        }

        public bool Decoding(string ParsedXML)
        {
            this.Version = Regex.Match(ParsedXML, "(?<=<Version>).*?(?=</Version>)", RegexOptions.IgnoreCase).Value;
            this.EncPrimitives = Regex.Match(ParsedXML, "(?<=<EncPrimitives>).*?(?=</EncPrimitives>)", RegexOptions.IgnoreCase).Value;
            this.SignPrimitives = Regex.Match(ParsedXML, "(?<=<SignPrimitives>).*?(?=</SignPrimitives>)", RegexOptions.IgnoreCase).Value;
            this.MACPrimitives = Regex.Match(ParsedXML, "(?<=<MACPrimitives>).*?(?=</MACPrimitives>)", RegexOptions.IgnoreCase).Value;
            this.MasterKeyHash = Regex.Match(ParsedXML, "(?<=<MasterKeyHash>).*?(?=</MasterKeyHash>)", RegexOptions.IgnoreCase).Value;
            this.Type = Regex.Match(ParsedXML, "(?<=<Type>).*?(?=</Type>)", RegexOptions.IgnoreCase).Value;
            this.Target = Regex.Match(ParsedXML, "(?<=<Target>).*?(?=</Target>)", RegexOptions.IgnoreCase).Value;
            this.MainBody = Regex.Match(ParsedXML, "(?<=<MainBody>).*?(?=</MainBody>)", RegexOptions.IgnoreCase).Value;
            this.IsSubsidiaryBodyLocked = bool.Parse(Regex.Match(ParsedXML, "(?<=<IsSubsidiaryBodyLocked>).*?(?=</IsSubsidiaryBodyLocked>)", RegexOptions.IgnoreCase).Value);
            this.SubsidiaryBodies = Regex.Match(ParsedXML, "(?<=<SubsidiaryBodies>).*?(?=</SubsidiaryBodies>)", RegexOptions.IgnoreCase).Value;
            this.TimeStart = Regex.Match(ParsedXML, "(?<=<TimeStart>).*?(?=</TimeStart>)", RegexOptions.IgnoreCase).Value;
            this.TimeSustain = int.Parse(Regex.Match(ParsedXML, "(?<=<TimeSustain>).*?(?=</TimeSustain>)", RegexOptions.IgnoreCase).Value);
            this.ExpressService = Regex.Match(ParsedXML, "(?<=<ExpressService>).*?(?=</ExpressService>)", RegexOptions.IgnoreCase).Value;
            this.NyarlathotepService = Regex.Match(ParsedXML, "(?<=<NyarlathotepService>).*?(?=</NyarlathotepService>)", RegexOptions.IgnoreCase).Value;
            this.NotesService = Regex.Match(ParsedXML, "(?<=<NotesService>).*?(?=</NotesService>)", RegexOptions.IgnoreCase).Value;
            this.ProsService = Regex.Match(ParsedXML, "(?<=<ProsService>).*?(?=</ProsService>)", RegexOptions.IgnoreCase).Value;
            this.InfonetService = Regex.Match(ParsedXML, "(?<=<InfonetService>).*?(?=</InfonetService>)", RegexOptions.IgnoreCase).Value;
            this.IsCoreUser = bool.Parse(Regex.Match(ParsedXML, "(?<=<IsCoreUser>).*?(?=</IsCoreUser>)", RegexOptions.IgnoreCase).Value);
            this.IsPayingUser = bool.Parse(Regex.Match(ParsedXML, "(?<=<IsPayingUser>).*?(?=</IsPayingUser>)", RegexOptions.IgnoreCase).Value);
            this.AdvancedSupport = int.Parse(Regex.Match(ParsedXML, "(?<=<AdvancedSupport>).*?(?=</AdvancedSupport>)", RegexOptions.IgnoreCase).Value);
            this.IsOverseasService = bool.Parse(Regex.Match(ParsedXML, "(?<=<IsOverseasService>).*?(?=</IsOverseasService>)", RegexOptions.IgnoreCase).Value);
            this.Region = Regex.Match(ParsedXML, "(?<=<Region>).*?(?=</Region>)", RegexOptions.IgnoreCase).Value;
            this.Comments = Regex.Match(ParsedXML, "(?<=<Comments>).*?(?=</Comments>)", RegexOptions.IgnoreCase).Value;
            return true;
        }
    }
}
