#ifndef __GAMESCENECOMPONENTS_DEFINITIONS_H__
#define __GAMESCENECOMPONENTS_DEFINITIONS_H__

#define GAME_NS_BEGIN namespace GameSceneElements {
#define GAME_NS_END }
#define USING_NS_GAME using namespace GameSceneElements

//Pass Gate Component
#define GATE_WIDTH 150
#define GATE_HEIGHT 150
#define GATE_ACTION_SPEED 0.3f

//Spikes Border Component
#define SPIKES_BORDER_SIZE 20

//
#define FREE_SPACE_SIZE 20 // free space around 'touch area'

//Score component
#define SCORE_TEXT_SIZE 70 // score layout height and label text size
#define SCORE_LAYOUT_BORDER_SIZE 15

//Components Name
#define SCORECOUNTER_NAME "ScoreCounter" // 'Score Counter Component'
#define PANEL_LEFT_NAME "TouchPanel_LEFT" // 'Touch Panel Component'
#define PANEL_RIGHT_NAME "TouchPanel_RIGHT" // 'Touch Panel Component'
#define SPIKESBORDER_NANE "SpikesBorder" // 'Spikes Border Component'
#define PLAYER_NAME "Player" // 'Player Component'
#define PASSGATE_NAME "PassGate" // 'Pass Gate Component'

//Components ID
#define PLAYER_ID 1 // 'Player Component'
#define SPIKESBORDER_ID 2 // 'Spikes Border Component'
#define PASSGATE_CENTER_ID 5 // 'Pass Gate Component'
#define PASSGATE_AREA_ID 6 // 'Pass Gate Component'
#define OBSTACLE_ID 7 // 'Pass Gate Component'
#define PANEL_LEFT_ID 10 // 'Touch Panel Component'
#define PANEL_RIGHT_ID 11 // 'Touch Panel Component'

#endif // __GAMESCENECOMPONENTS_DEFINITIONS_H__