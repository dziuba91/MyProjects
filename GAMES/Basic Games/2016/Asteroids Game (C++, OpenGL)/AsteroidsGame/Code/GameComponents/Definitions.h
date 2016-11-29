#ifndef __GAMECOMPONENTS_DEFINITIONS_H__
#define __GAMECOMPONENTS_DEFINITIONS_H__

// 'PlayerComponent.h'
#define PLAYER_SIZE 40
#define PLAYER_POSITION_OFFSET 50
#define PLAYER_MOVE_SPEED 10.0f

// 'ShootComponent.h'
#define SHOOT_FREQUENCY 200 // minimal time in milliseconds to next shoot
#define BULLET_SIZE 5.0f // must be less than 'PLAYER_SIZE'

// 'ShootComponent/Bullet.h'
#define BULLET_MOVE_SPEED 10.0f

// 'AsteroidsComponent/Ateroid.h'
#define ASTEROID_MAX_DIRECTIONAL_MOVE_SPEED 1.5f
#define ASTEROID_MIN_DIRECTIONAL_MOVE_SPEED 0.5f // must be less than 'ASTEROID_MAX_DIRECTIONAL_MOVE_SPEED'
#define ASTEROID_DIRECTIONAL_MOVE_SPEED_PRECISION 10000 // values of 'ASTEROID_MIN_DIRECTIONAL_MOVE_SPEED' and 'ASTEROID_MAX_DIRECTIONAL_MOVE_SPEED' must be greater than: 1/'ASTEROID_DIRECTIONAL_MOVE_SPEED_PRECISION'
#define ASTEROID_MAX_ROTATION_SPEED 5.0f
#define ASTEROID_MIN_ROTATION_SPEED 2.0f // must be less than 'ASTEROID_MAX_ROTATION_SPEED'
#define ASTEROID_ROTATION_SPEED_PRECISION 100 // values of 'ASTEROID_MIN_ROTATION_SPEED' and 'ASTEROID_MAX_ROTATION_SPEED' must be greater than: 1/'ASTEROID_ROTATION_SPEED_PRECISION'
#define ASTEROID_FALLING_MOVE_SPEED 5.0f

// 'AsteroidsComponent.h'
#define ASTEROID_START_NUMBER 2
#define ASTEROID_MAX_NUMBER 30
#define ASTEROID_MAX_SIZE 50 // maximal width, height
#define ASTEROID_MIN_SIZE 20 // minimal width, height; must be less than 'ASTEROID_MAX_SIZE'
#define ASTEROID_MIN_OFFSET 15 // minimal offset beetwen two 'Asteroids' (Y-axis)

// All game 'Components'
#define DEFAULT_THICKNESS PLAYER_SIZE

// Components ID
#define PLAYER_COMPONENT_ID 1 // 'PlayerComponent.h'
#define ASTEROIDS_COMPONENT_ID 2 // 'AsteroidsComponent.h'

#endif // __GAMECOMPONENTS_DEFINITIONS_H__