using System;

namespace SequoiaEngine
{
    public class RenderedComponent : Component
    {
        public RenderedComponent()
        {
            this.IsHUD = false;
        }

        public RenderedComponent(bool isHUD)
        {
            IsHUD = isHUD;
        }

        public bool IsHUD { get; set; }
    }
}