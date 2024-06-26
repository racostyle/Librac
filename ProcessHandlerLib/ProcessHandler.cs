﻿using System.Diagnostics;
using System.Threading.Tasks;

namespace Librac.ProcessHandlerLib
{
    /// <summary>
    /// Class for closing procesess
    /// </summary>
    public static class ProcessHandler
    {
        private static readonly IProcessHandler _processHandler = new ProcessHandlerMethods();

        /// <summary>
        /// Generates a PowerShell command to forcefully terminate processes by name, allowing for inclusion (name matches) and exclusion (name does not match prefixed with "!") criteria. 
        /// After Runs a powershell script and executes said command. It will kill all proceses which respect naming conditions
        /// <para>
        /// Example of arguments: ("test", "!production") will generate a script that kills all processes that contain "test" but do not contain "production"
        /// </para>
        /// </summary> 
        public static void Kill_Process_ByName(params string[] args)
        {
            _processHandler.Kill_Process_ByName(args);
        }
        /// <summary>
        /// Will search trough all opened procesess and kill a process with PID and TimeCreated saved in <paramref name="fullFileName"/> .txt file if processs has same 
        /// PID and TimeCreated
        /// </summary>
        public static void Kill_Process_ByPIDAndTimeCreated(string fullFileName)
        {
            _processHandler.Kill_Process_ByPIDAndTimeCreated(fullFileName);
        }
        /// <summary>
        /// Saves the ID and start time of a specified process to a file.
        /// </summary>
        /// <param name="process">The process whose information is to be saved.</param>
        /// <param name="fullFileName">The full path and name of the file where the process information will be saved.</param>
        /// <remarks>
        /// If the provided filename is null or empty, the method will exit without performing any action.
        /// The process ID and start time are saved in the format "ID|Start Time".
        /// </remarks>
        public static void SaveProcessInfo(Process process, string fullFileName)
        {
            _processHandler.SaveProcessInfo(process, fullFileName);
        }
        /// <summary>
        /// Terminates .NET processes whose full name or command line (full name of executing process) contains a specified filter string.
        /// </summary>
        /// <param name="filter">A string filter used to match against the process's full name or command line. 
        /// Only processes that contain this string in their full name or command line will be terminated.</param>
        public static void Kill_DotnetProcess_ByFullNameFilter(string filter)
        {
            _processHandler.Kill_DotnetProcess_ByFullNameFilter(filter);
        }

        /// <summary>
        /// Terminates processes whose full name or command line (full name of executing process) contains a specified filter string and are owned by the current user.
        /// </summary>
        /// <param name="filter">The filter to apply to the process names. Only processes whose command line includes this filter and are owned by the current user will be terminated.</param>
        /// <remarks>
        /// This method uses Windows Management Instrumentation (WMI) to query active processes and matches the command line of each process against the provided filter. 
        /// It ensures that only processes owned by the current user are targeted for termination, minimizing the risk of affecting system or other user's processes.
        /// </remarks>
        public static void Kill_CurrentUserProcess_ByFullNameFilter(string filter)
        {
            _processHandler.Kill_CurrentUserProcess_ByFullNameFilter(filter);
        }

        /// <summary>
        /// Terminates all processes that are listening on the specified TCP ports.
        /// </summary>
        /// <param name="ports">An array of integers representing the TCP ports. Processes listening on any of these ports will be terminated.</param>
        /// <remarks>
        /// This method uses the TCP connections from the local machine to identify and terminate processes based on the ports they are listening on. It 
        /// checks each connection to see if its local port is contained in the provided list of ports, and if so, attempts to terminate the associated process.
        /// </remarks>
        public static void Kill_Process_ByTcpPortListened(params int[] ports)
        {
            _processHandler.Kill_Process_ByTcpPortListened(ports);
        }
    }
}
