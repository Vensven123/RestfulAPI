namespace BwssbRestfulAPI.DBServices
{
    public class DBServiceClass : IDBServices
    {

        private readonly string BGSConn;
        private readonly string BangaloreOneConn;
        private readonly string BBPSConn;
        private readonly string OnlineBGS;
        private readonly string BackupBGS;
        private readonly string bwssbonlinepg;


        public DBServiceClass(IConfiguration configuration) 
        {

            BGSConn = configuration.GetConnectionString("BGS");
            BangaloreOneConn = configuration.GetConnectionString("DefaultConnection");
            BBPSConn = configuration.GetConnectionString("BBPS");
            OnlineBGS = configuration.GetConnectionString("OnlineBGS");
            BackupBGS = configuration.GetConnectionString("BackupBGS");
            bwssbonlinepg = configuration.GetConnectionString("bwssbonlinepg");
            
        }

        public string GetBGSConn() => BGSConn;
        public string GetBackupBGSCon() => BackupBGS;
        public string GetBangaloreOneCon() => BangaloreOneConn;
        public string GetBBPSCon() => BBPSConn;   
        public string GetOnlineBGSCon() => OnlineBGS;
        public string GetbwssbonlinepgCon() => bwssbonlinepg;

        
    }
}
