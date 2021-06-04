/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID AMB_BIRDS = 1573457378U;
        static const AkUniqueID AMB_BUSH_RUSTLE_ENTER = 2857601955U;
        static const AkUniqueID AMB_FIRE = 3207324726U;
        static const AkUniqueID AMB_FLYING_BIRDS = 3910014366U;
        static const AkUniqueID AMB_SCHROOM_ENTER = 4025637178U;
        static const AkUniqueID AMB_WATERFALL = 3871099588U;
        static const AkUniqueID CHAR_COUGH_GAS = 2562373584U;
        static const AkUniqueID CHAR_FOOTSTEP_DASH = 3864694805U;
        static const AkUniqueID CHAR_FOOTSTEPS = 3331452589U;
        static const AkUniqueID CHAR_KNIFE_SWINGING = 3862022932U;
        static const AkUniqueID CHAR_TAKING_DAMAGE = 120190188U;
        static const AkUniqueID CHAR_VOICE_DEATH = 3627586745U;
        static const AkUniqueID MUSIC_AMBIENT_DRONE = 205229924U;
        static const AkUniqueID SELLER_MMM = 4011189322U;
        static const AkUniqueID SFX_BOX_DESTROY = 490354319U;
        static const AkUniqueID SFX_BUY_SHOP = 2554305358U;
        static const AkUniqueID SFX_CHANGE_COLOR = 171029537U;
        static const AkUniqueID SFX_CROSSBOW_SHOOT = 3500565137U;
        static const AkUniqueID SFX_END_ATTACK_COOLDOWN = 3677607289U;
        static const AkUniqueID SFX_OBJ_BUILD_TRAP = 318795889U;
        static const AkUniqueID SFX_OBJ_BURNING_FUSE = 3544644124U;
        static const AkUniqueID SFX_OBJ_CAMO = 4108944039U;
        static const AkUniqueID SFX_OBJ_DETECTOR = 919196649U;
        static const AkUniqueID SFX_OBJ_GAS = 1107926442U;
        static const AkUniqueID SFX_OBJ_GEL = 1175037001U;
        static const AkUniqueID SFX_OBJ_GETTING_INTO_BINDING_TRAP = 3736381994U;
        static const AkUniqueID SFX_OBJ_GETTING_INTO_GEL_TRAP = 4186763055U;
        static const AkUniqueID SFX_OBJ_PICKUP = 1509706435U;
        static const AkUniqueID SFX_OBJ_RADAR_USE = 601085705U;
        static const AkUniqueID SFX_OBJ_SETTING_GEL_TRAP = 1201681274U;
        static const AkUniqueID SFX_OBJ_SPEEDPOTION = 2639401961U;
        static const AkUniqueID SFX_OBJ_SPIKE_TRAP_ACTIVATE = 4116042345U;
        static const AkUniqueID SFX_OBJ_SPIKEPIT = 2667521114U;
        static const AkUniqueID SFX_OBJ_THROW = 3927170345U;
        static const AkUniqueID SFX_OBJ_TRIPWIRE = 4163172313U;
        static const AkUniqueID SFX_OBJ_TRIPWIRE_ACTIVATE = 3325140479U;
        static const AkUniqueID SFX_OBJ_VISIONPOTION = 4096920342U;
        static const AkUniqueID SFX_PICKUP_GOLD = 237514306U;
        static const AkUniqueID SFX_SUPPLIES = 136158832U;
        static const AkUniqueID SFX_TELEPORT = 4028214536U;
        static const AkUniqueID UI_CLICK_WOOD_PANEL_EXIT = 3464476944U;
        static const AkUniqueID UI_CLOSE_MAP = 2785568501U;
        static const AkUniqueID UI_COUNTDOWN = 3267870591U;
        static const AkUniqueID UI_INTERACT = 4294591936U;
        static const AkUniqueID UI_OPEN_MAP = 2149148881U;
        static const AkUniqueID UI_START_GAME = 3730899723U;
        static const AkUniqueID UI_WALLS_DOWN = 2373628060U;
        static const AkUniqueID UI_WOOD_OPEN_PANEL = 3080149663U;
        static const AkUniqueID UI_WOOD_PANEL_OPTIONS_CHECKBOX = 601065099U;
        static const AkUniqueID UICLICKWOODPANEL = 2097608532U;
        static const AkUniqueID WIND = 1537061107U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace BUSH
        {
            static const AkUniqueID GROUP = 1427625975U;

            namespace STATE
            {
                static const AkUniqueID BIG = 647611021U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID SMALL = 1846755610U;
            } // namespace STATE
        } // namespace BUSH

        namespace FOOTSTEP
        {
            static const AkUniqueID GROUP = 1866025847U;

            namespace STATE
            {
                static const AkUniqueID INAUDIBLE = 2437297550U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID NORMAL = 1160234136U;
                static const AkUniqueID SPEED = 640949982U;
            } // namespace STATE
        } // namespace FOOTSTEP

    } // namespace STATES

    namespace SWITCHES
    {
        namespace FOOSTEP_INTENSITY
        {
            static const AkUniqueID GROUP = 231967287U;

            namespace SWITCH
            {
                static const AkUniqueID CALM = 3753286132U;
                static const AkUniqueID LOUD = 321898433U;
            } // namespace SWITCH
        } // namespace FOOSTEP_INTENSITY

        namespace LOCATION
        {
            static const AkUniqueID GROUP = 1176052424U;

            namespace SWITCH
            {
                static const AkUniqueID FOREST = 491961918U;
                static const AkUniqueID RUINS = 417916826U;
            } // namespace SWITCH
        } // namespace LOCATION

        namespace SURFACE
        {
            static const AkUniqueID GROUP = 1834394558U;

            namespace SWITCH
            {
                static const AkUniqueID GRASS = 4248645337U;
                static const AkUniqueID STONE = 1216965916U;
                static const AkUniqueID WATER = 2654748154U;
                static const AkUniqueID WOOD = 2058049674U;
            } // namespace SWITCH
        } // namespace SURFACE

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID NEW_GAME_PARAMETER = 3671138082U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAIN = 3161908922U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID NEW_AUDIO_BUS = 2255513057U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
