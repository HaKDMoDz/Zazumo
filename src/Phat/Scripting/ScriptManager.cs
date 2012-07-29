using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;

namespace Phat.Scripting
{
    public class ScriptManager
    {
        private readonly IProcessRunner _processRunner;
        private readonly Dictionary<String, Type> _scripts;

        public ScriptManager(IProcessRunner processRunner)
        {
            this._processRunner = processRunner;

            this._scripts = new Dictionary<String, Type>();
        }

        public void RegisterScript<TScript>(String scriptName)
            where TScript : Script
        {
            _scripts[scriptName] = typeof(TScript);
        }

        public void RegisterScript(String scriptName, Type scriptType)
        {
            _scripts[scriptName] = scriptType;
        }

        public void ExecuteScript(String scriptName)
        {
            var scriptType = _scripts[scriptName];

            var script = (Script)Activator.CreateInstance(scriptType);

            var activityContainer = new ScriptActivityContainer();
            script.BuildScript(activityContainer);

            var context = new ScriptExecutionContext();

            ((IScriptActivityExecutor)context).ExecuteActivities(activityContainer);

            _processRunner.ScheduleProcess(context);
        }
    }
}
