#include "ShootComponent.h"

#include "PlayerComponent.h"
#include "Definitions.h"

USING_NS_ENGINE;

void ShootComponent::init()
{
	this->_lastShootTime = 0;
}

void ShootComponent::draw()
{
	for (auto tmp : this->_bulletList) tmp->draw();
}

void ShootComponent::update()
{
	// add 'Bullet'
	if (Application::isKeyDown(Keys::SPACE) && 
		(!this->_lastShootTime || ((GetTickCount() - this->_lastShootTime) >= SHOOT_FREQUENCY)))
			this->addBullet();

	// remove 'Bullet'
	if (this->_bulletList.size() && 
		((_bulletList[0]->getPosition().y + _bulletList[0]->getSize().y/2) > Application::getWindowSize().height/2))
			this->removeBullet();

	// update parameters in the 'Bullet' array
	for (auto tmp : this->_bulletList) tmp->update();
}

void ShootComponent::reset()
{
	this->clearBulletList();

	this->init();
}

void ShootComponent::onExit()
{
	this->clearBulletList();
}

void ShootComponent::removeBullet()
{
	if (this->_bulletList.size())
	{
		delete this->_bulletList[0];

		this->_bulletList.erase(this->_bulletList.begin());
	}
}

void ShootComponent::addBullet()
{
	this->_bulletList.push_back(new Bullet(Vector3D(BULLET_SIZE), 
		((PlayerComponent*)Game::getComponent(PLAYER_COMPONENT_ID))->getSpaceship()->getPosition()));

	this->_lastShootTime = GetTickCount();
}

void ShootComponent::clearBulletList()
{
	for (auto tmp : this->_bulletList) delete tmp;

	this->_bulletList.clear();
}