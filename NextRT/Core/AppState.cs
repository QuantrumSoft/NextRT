using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextRT.Core
{
    public class AppState
    {
        public string Name = "";
        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void Render() { }
        public virtual void Stop() { }
        public virtual void Pause() { }
        public virtual void Resume() { }

    }
}
