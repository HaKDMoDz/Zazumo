using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Phat.Scripting
{
    public class ScriptActivityContainer : IEnumerable<ScriptActivity>
    {
        private readonly List<ScriptActivity> _activities;

        public ScriptActivityContainer()
        {
            this._activities = new List<ScriptActivity>();
        }

        public void AddActivity(ScriptActivity activity)
        {
            _activities.Add(activity);
        }

        public IEnumerator<ScriptActivity> GetEnumerator()
        {
            return _activities.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_activities).GetEnumerator();
        }
    }
}
