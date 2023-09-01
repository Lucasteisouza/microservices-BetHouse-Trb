using TryBets.Odds.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Globalization;

namespace TryBets.Odds.Repository;

public class OddRepository : IOddRepository
{
    protected readonly ITryBetsContext _context;
    public OddRepository(ITryBetsContext context)
    {
        _context = context;
    }

    public Match Patch(int MatchId, int TeamId, string BetValue)
    {
        Match foundMatch = _context.Matches.FirstOrDefault(m => m.MatchId == MatchId);
        Team foundTeam = _context.Teams.FirstOrDefault(t => t.TeamId == TeamId);
        if (foundMatch == null || foundTeam == null)
        {
            throw new Exception("Match or Team not found");
        }
        if (foundMatch.MatchTeamAId != TeamId && foundMatch.MatchTeamBId != TeamId)
        {
            throw new Exception("Team not found in match");
        }
        string newBetValueString = BetValue.Replace(",", ".");
        decimal newBetValue = decimal.Parse(newBetValueString, CultureInfo.InvariantCulture);
        if (foundMatch.MatchTeamAId == TeamId)
        {
            foundMatch.MatchTeamAValue += newBetValue;
        }
        else
        {
            foundMatch.MatchTeamBValue += newBetValue;
        }
        _context.Matches.Update(foundMatch);
        _context.SaveChanges();
        return foundMatch;
    }
}