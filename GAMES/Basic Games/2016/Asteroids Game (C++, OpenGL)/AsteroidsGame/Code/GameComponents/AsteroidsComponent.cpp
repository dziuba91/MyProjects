#include "AsteroidsComponent.h"

#include "GameComponents/Definitions.h"
#include "GameComponents/PlayerComponent.h"

USING_NS_ENGINE;

void AsteroidsComponent::init()
{
	this->setComponentID(ASTEROIDS_COMPONENT_ID);

	this->_asteroidsPerRound = ASTEROID_START_NUMBER;
}

void AsteroidsComponent::draw()
{
	for (auto tmp : this->_asteroidList) tmp->draw();
}

void AsteroidsComponent::update()
{
	// add 'Asteroid'
	if (!this->_asteroidList.size())
		this->addAsteroid();
	else if (this->_asteroidList.size() < this->_asteroidsPerRound)
	{
		Asteroid* tmp = this->_asteroidList[_asteroidList.size()-1];

		if ((tmp->getPosition().y + tmp->getSize().y/2 + ASTEROID_MIN_OFFSET) < Application::getWindowSize().height/2)
			this->addAsteroid();
	}

	// remove 'Asteroid'
	if ((_asteroidList[0]->getPosition().y + _asteroidList[0]->getSize().y / 2) <
		(((PlayerComponent*)Game::getComponent(PLAYER_COMPONENT_ID))->getSpaceship()->getPosition().y - PLAYER_SIZE/2))
			this->removeAsteroid(0);

	// update 'Asteroid' parameters
	for (auto tmp : this->_asteroidList) tmp->update();
}

void AsteroidsComponent::reset()
{
	this->clearAsteroidList();

	this->_asteroidsPerRound = ASTEROID_START_NUMBER;
}

void AsteroidsComponent::onExit()
{
	this->clearAsteroidList();
}

void AsteroidsComponent::clearAsteroidList()
{
	for (auto tmp : this->_asteroidList) delete tmp;

	this->_asteroidList.clear();
}

void AsteroidsComponent::removeAsteroid(int index)
{
	if (index < this->_asteroidList.size())
	{
		delete this->_asteroidList[index];

		this->_asteroidList.erase(this->_asteroidList.begin() + index);
	}
}

void AsteroidsComponent::addAsteroid()
{
	Vector3D size = Vector3D(rand() % (ASTEROID_MAX_SIZE - ASTEROID_MIN_SIZE) + ASTEROID_MIN_SIZE,
		rand() % (ASTEROID_MAX_SIZE - ASTEROID_MIN_SIZE) + ASTEROID_MIN_SIZE, DEFAULT_THICKNESS);

	Vector3D position((int)(rand() % (int)(Application::getWindowSize().width - 2*size.x) - Application::getWindowSize().width/2 + size.x), 
		(int)(Application::getWindowSize().height/2 + size.y/2), -2*DEFAULT_THICKNESS);

	this->_asteroidList.push_back(new Asteroid(size, position));
}

const std::vector<Asteroid*>& AsteroidsComponent::getAsteroidList()
{
	return this->_asteroidList;
}

void AsteroidsComponent::shootToAsteroid(int index)
{
	this->removeAsteroid(index);

	if (this->_asteroidsPerRound <= ASTEROID_MAX_NUMBER)
		this->_asteroidsPerRound++;
}