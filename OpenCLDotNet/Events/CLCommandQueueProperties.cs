using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Events
{
    /// <summary>
    /// 
    /// </summary>
    public struct CLCommandQueueProperties
    {
        /// <summary>
        /// 
        /// </summary>
        public CL_COMMAND_QUEUE_POPERTIES Properties;

        /// <summary>
        /// 
        /// </summary>
        public cl_uint QueueSize;

        /// <summary>
        /// 
        /// </summary>
        public CLCommandQueueProperties()
        {
            Properties = 0;
            QueueSize = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public static CLCommandQueueProperties Default
        {
            get
            {
                var param = new CLCommandQueueProperties();
                return param;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("[CLCommandQueueProperties: Properties={0}, QueueSize={1},]",
                Properties, QueueSize);
        }

    }

}

