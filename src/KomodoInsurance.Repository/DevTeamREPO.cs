using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class DevTeamREPO
{
    private readonly List<DevTeam> _devTeamDataBase = new List<DevTeam>();
    private int _count = 0;

    public bool AddDevTeamToDatabase(DevTeam devTeam)
    {
        if(devTeam != null)
        {
            _count++;
            devTeam.TeamID = _count;
            _devTeamDataBase.Add(devTeam);
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<DevTeam> GetAllTeams()
    {
        return _devTeamDataBase;
    }

    public DevTeam GetDevTeamByID(int id)
    {
        foreach(DevTeam t in _devTeamDataBase)
        {
            if(t.TeamID == id)
            {
                return t;
            }
        }

        return null;
    }

    public bool UpdateDevTeamData(int TeamID, DevTeam newDevTeamData)
    {
        var oldDevTeamData = GetDevTeamByID(TeamID);
        if(oldDevTeamData != null)
        {
            oldDevTeamData.Name = newDevTeamData.Name;
            oldDevTeamData.Developer = newDevTeamData.Developer;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool RemoveDevTeamFromDatabase(int id)
    {
        var devTeam = GetDevTeamByID(id);
        if(devTeam != null)
        {
            _devTeamDataBase.Remove(devTeam);
            return true;
        }
        else
        {
            return false;
        }
    }
}
