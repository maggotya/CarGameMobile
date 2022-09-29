using System;
using System.Collections.Generic;
using UnityEngine;

using Object = UnityEngine.Object;

internal abstract class BaseController : IDisposable
{
    private List<BaseController> _baseControllers;
    private List<IRepository> _repositories;
    private List<GameObject> _gameObjects;
    private bool _isDisposed;


    public void Dispose()
    {
        if (_isDisposed)
            return;

        _isDisposed = true;

        DisposeBaseControllers();
        DisposeRepositories();
        DisposeGameObjects();

        OnDispose();
    }

    private void DisposeBaseControllers()
    {
        if (_baseControllers == null)
            return;

        foreach (BaseController baseController in _baseControllers)
            baseController.Dispose();

        _baseControllers.Clear();
    }

    private void DisposeRepositories()
    {
        if (_repositories == null)
            return;

        foreach (IRepository repository in _repositories)
            repository.Dispose();

        _repositories.Clear();
    }

    private void DisposeGameObjects()
    {
        if (_gameObjects == null)
            return;

        foreach (GameObject gameObject in _gameObjects)
            Object.Destroy(gameObject);

        _gameObjects.Clear();
    }

    protected virtual void OnDispose() { }


    protected void AddController(BaseController baseController)
    {
        _baseControllers ??= new List<BaseController>();
        _baseControllers.Add(baseController);
    }

    protected void AddRepository(IRepository repository)
    {
        _repositories ??= new List<IRepository>();
        _repositories.Add(repository);
    }

    protected void AddGameObject(GameObject gameObject)
    {
        _gameObjects ??= new List<GameObject>();
        _gameObjects.Add(gameObject);
    }
}
