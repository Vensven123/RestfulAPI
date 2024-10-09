namespace BwssbRestfulAPI.DBServices
{
    public interface IDBServices
    {
        string GetBGSConn();
        string GetBBPSCon();

        string GetbwssbonlinepgCon();

        string GetBackupBGSCon();

        string GetOnlineBGSCon();

        string GetBangaloreOneCon();


    }
}
