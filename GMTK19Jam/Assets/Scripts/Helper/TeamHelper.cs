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
            case Team.One:
                return Team.A;
            case Team.A:
                return Team.One;
            default:
                throw new Exception("Goal::GetOppositeTeam ERROR: INVALID TEAM");
        }
    }
}

