namespace QuizWebsite.Data
{
    internal class ConnectionBucket
    {

#if JAHREL
        public const string ConnectionString = "data source=JAHREL-PC\\SQLEXPRESS;initial catalog=QuizWebsite;persist security info=False;connect timeout=1000;integrated security=SSPI;encrypt=False";
#else
        public const string ConnectionString = "data source=BLD\\SQLEXPRESS;initial catalog=QuizWebsite;persist security info=False;connect timeout=1000;integrated security=SSPI;encrypt=False";
#endif
    }
}
