using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.constant
{
    class constant
    {
        public const float SCREEN_WIDTH = 1920f;
        public const float SCREEN_HEIGHT = 1080f;
        public const float MAX_PLAYER_MOVABLE_HEIGHT = 2f;
        public const float MIN_PLAYER_MOVABLE_HEIGHT = -2f;
        public const float MAX_PLAYER_MOVABLE_WIDTH = 2f;
        public const float MIN_PLAYER_MOVABLE_WIDTH = -2f;

        public const float MAX_ITEM_GENERATION_HEIGHT = MAX_PLAYER_MOVABLE_HEIGHT * 0.8F;
        public const float MIN_ITEM_GENERATION_HEIGHT = MIN_PLAYER_MOVABLE_WIDTH;
        public const float MAX_ITEM_GENERATION_WIDTH = MAX_PLAYER_MOVABLE_WIDTH;
        public const float MIN_ITEM_GENERATION_WIDTH = MIN_PLAYER_MOVABLE_WIDTH;
    }
}
