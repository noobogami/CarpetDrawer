namespace HamiIO.Cs
{
    public class MethodsModel
    {
        public AccessModifiers AccessModifier;
        public string Name;
        public string ReturnType;
        public ArgumentModel[] Arguments;
        public bool IsOverride;
        public bool IsVirtual;
        public bool IsStatic;
        public string[] Attribute;
        public string Body;
    }
}