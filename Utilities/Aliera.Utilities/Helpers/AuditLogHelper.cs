using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.AspNetCore.Http;

namespace Aliera.Utilities.AuditLog
{
    public class AuditLogHelper
    {
        private static Queue<object> _auditQueue = new Queue<object>();      

        public static Timer Timer = new Timer();
        public static string AuditApiUrl { get; set; }
        public static int AuditQueueLimit { get; set; } = 0;
        public static string AuditRequestApi { get; set; }
        public static int AuditSetTimeout { get; set; } = 0;

        /// <summary>
        /// Queue audits and save to Audit DB once the queue reaches the limit 
        /// </summary>
        /// <param name="auditBO"></param>
        /// <returns></returns>
        public static async Task QueueAudits(object auditBO)
        {
            _auditQueue.Enqueue(auditBO);
            if (_auditQueue.Count == AuditQueueLimit)
            {
                IList<object> auditsToPost = _auditQueue.ToArray().ToList();
                _auditQueue.Clear();
                await PostAsync(auditsToPost);
            }
        }

        /// <summary>
        /// Timed event to post audit logs from the queue
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public static async void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Timer.Stop();
            IList<object> auditsToPost = _auditQueue.ToArray().ToList();
            _auditQueue.Clear();
            await PostAsync(auditsToPost);           
            Timer.AutoReset = true;
            Timer.Start();
        }               

        /// <summary>
        /// Method to post audits via HttpClient request
        /// </summary>
        /// <param name="audits"></param>
        /// <returns></returns>
        private static async Task PostAsync(IList<object> audits)
        {
            //Post  - Fire and forget (exceptions handled in AuditService) 
            await UtilityHelper.PostAsync(AuditApiUrl, AuditRequestApi, audits);
        }
    }
}
