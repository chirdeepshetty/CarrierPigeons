using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace DomainModel
{
    public class MatchRepository : RepositoryBase, IMatchRepository
    {

        private MatchRepository()
        {
        }
        static MatchRepository _matchRepository = new MatchRepository();

        public static IMatchRepository Instance
        {
            get
            {
                _matchRepository.SetSession();
                return _matchRepository;
            }
        }

 
        
        #region IMatchRepository Members

        public void Save(Match match)
        {
            Session.Save(match);
        }


        public void Delete(Match match)
        {
            Session.Delete(match);
        }

        public Match Load(int matchId)
        {
            Match match = new Match();
            Session.Load(match, matchId);
            return match;
            
        }

        public IList<Match> LoadMatchesByUserRequest(string emailAddress)
        {
            const string querystring = "from Match as M  where M.Request.RequestedUser.Email.EmailAddress = :user_id";
            IQuery query = Session.CreateQuery(querystring);
            query.SetString("user_id", emailAddress);
            var matchList = (List<Match>)query.List<Match>();
            return matchList;
        }

        public IList<Match> LoadMatchesByUserJourney(string emailAddress)
        {

            const string querystring = "from Match as M  where M.Journey.Traveller.Email.EmailAddress = :user_id";
            IQuery query = Session.CreateQuery(querystring);
            query.SetString("user_id", emailAddress);
            var matchList = (List<Match>)query.List<Match>();
            return matchList;
        }

        public void UpdateMatches(IList<Match> matches)
        {
            foreach (Match match in matches)
            {
                Session.SaveOrUpdate(match);

            }
        }

        #endregion

    }

    public interface IMatchRepository
    {
        void Save(Match match);
        Match Load(int matchId);
        void Delete(Match match);
        IList<Match> LoadMatchesByUserRequest(string emailAddress);
        IList<Match> LoadMatchesByUserJourney(string emailAddress);
        void UpdateMatches(IList<Match> matches);

    }
}