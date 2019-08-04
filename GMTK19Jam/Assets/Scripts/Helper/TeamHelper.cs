using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class TeamHelper 
{
    static public Team GetOppositeTeam(Team myTeam) 
    {
        switch (myTeam) 
        {
            case Team.P1:
                return Team.P2;
            case Team.P2:
                return Team.P1;
            default:
                throw new Exception("Goal::GetOppositeTeam ERROR: INVALID TEAM");
        }
    }
}

