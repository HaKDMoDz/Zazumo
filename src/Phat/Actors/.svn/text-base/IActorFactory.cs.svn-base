using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Phat
{
    /// <summary>
    /// When implemented, builds and initializes all actors.
    /// </summary>
    /// <remarks>
    /// Always use the actor factory to instantiate actors.  Instantiating by with the "new"
    /// keyword will not register the actor with subsystems.
    /// </remarks>
    public interface IActorFactory
    {
        /// <summary>
        /// Creates an actor of type TActor with no default data.
        /// </summary>
        /// <typeparam name="TActor">The Type of actor to create</typeparam>
        /// <returns>Returns an initialized, ready to use actor.</returns>
        TActor Create<TActor>()
            where TActor : Actor;
        
        /// <summary>
        /// Creates an actor of type TActor with default data defined in a loaded archetype resource.
        /// </summary>
        /// <typeparam name="TActor">The Type of actor to create.</typeparam>
        /// <param name="archetypeKey">The archetype key to load from resources.</param>
        /// <returns>Returns an initialized, ready to use actor.</returns>
        TActor Create<TActor>(String archetypeKey)
            where TActor : Actor;

        /// <summary>
        /// Creates an actor of type TActor passing in a default data object.
        /// </summary>
        /// <typeparam name="TActor">The Type of actor to create.</typeparam>
        /// <param name="defaultData">The data to initialize the object to.</param>
        /// <returns>Returns an initialized, ready to use actor.</returns>
        TActor Create<TActor>(Object defaultData)
            where TActor : Actor;

        /// <summary>
        /// Creates an actor of type TActor with default data defined in a loaded archetype resource.
        /// </summary>
        /// <typeparam name="TActor">The Type of actor to create.</typeparam>
        /// <param name="archetypeKey">The archetype key to load from resources.</param>
        /// <param name="location">The initial location of the actor.</param>
        /// <returns>Returns an initialized, ready to use actor.</returns>
        TActor Create<TActor>(String archetypeKey, Vector2 location)
            where TActor : Actor;

        /// <summary>
        /// Creates an actor of type TActor passing in a default data object.
        /// </summary>
        /// <typeparam name="TActor">The Type of actor to create.</typeparam>
        /// <param name="defaultData">The data to initialize the object to.</param>
        /// <param name="location">The initial location of the actor.</param>
        /// <returns>Returns an initialized, ready to use actor.</returns>
        TActor Create<TActor>(Object defaultData, Vector2 location)
            where TActor : Actor;
        
        /// <summary>
        /// Creates an actor.
        /// </summary>
        /// <param name="actorTypeName">The type of actor to instantiate.</param>
        /// <returns>Returns an initialized, ready to use actor.</returns>
        Actor Create(String actorTypeName);

        /// <summary>
        /// Creates an actor.
        /// </summary>
        /// <param name="actorTypeName">The type of actor to instantiate.</param>
        /// <returns>Returns an initialized, ready to use actor.</returns>
        Actor Create(String actorTypeName, String archetypeKey);

        /// <summary>
        /// Creates an actor.
        /// </summary>
        /// <param name="actorTypeName">The type of actor to instantiate.</param>
        /// <param name="defaultData">The data to initialize the object to.</param>
        /// <returns>Returns an initialized, ready to use actor.</returns>
        Actor Create(String actorTypeName, Object defaultData);

        /// <summary>
        /// Creates an actor.
        /// </summary>
        /// <param name="actorTypeName">The type of actor to instantiate.</param>
        /// <param name="archetypeKey">The archetype key to load from resources.</param>
        /// <param name="location">The initial location of the actor.</param>
        /// <returns>Returns an initialized, ready to use actor.</returns>
        Actor Create(String actorTypeName, String archetypeKey, Vector2 location);

        /// <summary>
        /// Creates an actor.
        /// </summary>
        /// <param name="actorTypeName">The type of actor to instantiate.</param>
        /// <param name="archetypeKey">The archetype key to load from resources.</param>
        /// <param name="defaultData">The data to initialize the object to.</param>
        /// <param name="location">The initial location of the actor.</param>
        /// <returns>Returns an initialized, ready to use actor.</returns>
        Actor Create(String actorTypeName, Object defaultData, Vector2 location);

    }
}
