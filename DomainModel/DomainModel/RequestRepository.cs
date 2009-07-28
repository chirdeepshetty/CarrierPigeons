using System;

namespace DomainModel
{
    public class RequestRepository : IRequestRepository
    {
        private RequestRepository(){}
        static RequestRepository _requestRepository = new RequestRepository();
        
        public static IRequestRepository Instance
        {
            get
            {
                return _requestRepository;
            }
        }

        #region IRequestRepository Members

        public void Save(Request request)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}