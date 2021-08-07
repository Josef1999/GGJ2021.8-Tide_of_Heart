using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class constant
{
    public static Vector3 ICON_POS { get { return new Vector3(3.4f, 4.6f); } }
    public static Vector3 ICON_SCALE { get { return new Vector3(0.5f, 0.5f); } }
    public const float SCREEN_WIDTH = 1920f;
    public const float SCREEN_HEIGHT = 1080f;
    public const float MAX_PLAYER_MOVABLE_HEIGHT = 1.6f;
    public const float MIN_PLAYER_MOVABLE_HEIGHT = -3.5f;
    public const float MAX_PLAYER_MOVABLE_WIDTH = 4.6f;
    public const float MIN_PLAYER_MOVABLE_WIDTH = -4.6f;

    public const float MAX_ITEM_GENERATION_HEIGHT = MAX_PLAYER_MOVABLE_HEIGHT * 0.8F;
    public const float MIN_ITEM_GENERATION_HEIGHT = MIN_PLAYER_MOVABLE_HEIGHT;
    public const float MAX_ITEM_GENERATION_WIDTH = MAX_PLAYER_MOVABLE_WIDTH;
    public const float MIN_ITEM_GENERATION_WIDTH = MIN_PLAYER_MOVABLE_WIDTH;

    public const int MIN_ROW = -3;
    public const int MAX_ROW = 2;
    public const int MIN_COL = -4;
    public const int MAX_COL = 5;
}

