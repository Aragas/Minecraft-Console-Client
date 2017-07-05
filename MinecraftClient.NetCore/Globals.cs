namespace MinecraftClient
{
    public class Globals
    {
        public CSharpAPI MCC { get; private set; }
        public string[] args { get; private set; }

        public Globals(CSharpAPI cSharpApi, string[] args)
        {
            MCC = cSharpApi;
            this.args = args;
        }
    }
}
