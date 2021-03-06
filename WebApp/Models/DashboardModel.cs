﻿
using Microsoft.Azure.IoTSuite.Connectedfactory.WebApp.Contoso;
using System;
using System.Collections.Generic;

namespace Microsoft.Azure.IoTSuite.Connectedfactory.WebApp.Models
{
    /// <summary>
    /// A view model for the Dashboard view.
    /// </summary>
    public class DashboardModel
    {
        /// <summary>
        /// The ID of the active session.
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// The node in the topology the view is visualising.
        /// </summary>
        public ContosoTopologyNode TopNode { get; set; }

        ///eventhub에서 가져올 데이터
        //public ContosoOpcUaNode EventNode { get; set; }

        public Type ChildrenType { get; set; }

        public List<ContosoChildInfo> Children;


        /// <summary>
        /// The Azure maps license key.
        /// </summary>
        public string MapApiQueryKey { get; set; }

        /// <summary>
        /// Header text of the containment of the children list of the current view.
        /// </summary>
        public string ChildrenContainerHeader { get; set; }

        /// <summary>
        /// List header text of the status column.
        /// </summary>
        public string ChildrenListHeaderStatus { get; set; }

        /// <summary>
        /// List header text of the location column.
        /// </summary>
        public string ChildrenListHeaderLocation { get; set; }

        /// <summary>
        /// List header text of the details column.
        /// </summary>
        public string ChildrenListHeaderDetails { get; set; }

        // <summary>
        /// List header text of the warning column.
        /// </summary>
        public string ChildrenListHeaderWarning { get; set; }


        

        /// <summary>
        /// List with alerts for the view.
        /// </summary>
        public List<ContosoAlertInfo> Alerts;
        
        /// <summary>
        /// Initializes a new instance of the DashboardModel class.
        /// </summary>
        public DashboardModel()
        {
            Children = new List<ContosoChildInfo>();
            Alerts = new List<ContosoAlertInfo>();
        }
    }
}