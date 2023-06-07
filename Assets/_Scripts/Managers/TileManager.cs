using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [Header("Asset References")]
    [SerializeField] private Sprite TileSpriteSquare;
    [SerializeField] private Sprite TileSpriteStar;
    [SerializeField] private Sprite TileSpriteSpecial;

    public static TileManager s_instance;
    public TileSlot SelectedTileSlot;

    private void Initialize() {
        SelectedTileSlot = null;
    }

    // TODO: Complete these rules
    public bool TileIsValid(TilePool tilePool, TileType tile) {
        GameManager gameManager = GameManager.s_instance;

        if (tile == TileType.PinkStar || tile == TileType.OrangeStar || tile == TileType.YellowStar || tile == TileType.GreenStar || tile == TileType.BlueStar) {
            if (gameManager.RoundsRemaining > 18 || gameManager.RoundsRemaining < 5) {
                return false;
            }
        }

        if (tile == TileType.Special) {
            if (gameManager.RoundsRemaining > 18 || gameManager.RoundsRemaining < 4) {
                return false;
            }
        }

        return true;
    }

    public Sprite TileTypeToSprite(TileType tileType) {
        switch (tileType) {
            case TileType.Pink or TileType.Orange or TileType.Yellow or TileType.Green or TileType.Blue:
                return TileSpriteSquare;
            case TileType.PinkStar or TileType.OrangeStar or TileType.YellowStar or TileType.GreenStar or TileType.BlueStar:
                return TileSpriteStar;
            case TileType.Special:
                return TileSpriteSpecial;
            default:
                return null;
        }
    }

    public Color32 TileTypeToColor(TileType tileType) {
        switch (tileType) {
            case TileType.Pink or TileType.PinkStar:
                return ColorManager.s_colorMap[TileType.Pink];
            case TileType.Orange or TileType.OrangeStar:
                return ColorManager.s_colorMap[TileType.Orange];
            case TileType.Yellow or TileType.YellowStar:
                return ColorManager.s_colorMap[TileType.Yellow];
            case TileType.Green or TileType.GreenStar:
                return ColorManager.s_colorMap[TileType.Green];
            case TileType.Blue or TileType.BlueStar:
                return ColorManager.s_colorMap[TileType.Blue];
            default:
                return new Color32(255, 255, 255, 255);
        }
    }
}
