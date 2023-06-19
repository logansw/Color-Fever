using UnityEngine;
/// <summary>
/// Abstract Data Type representing a timeline of states for the specified object
/// <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Timeline<T>
{
    // The number of frames in the timeline.
    private int _size;
    // The data within the timeline.
    private T[] _frames;
    // The current frame.
    private int _currentFrame;
    // The frame furthest back in the timeline.
    private int _backFrame;
    // The frame furthest forward in the timeline.
    private int _frontFrame;

    /// <summary>
    /// Constructs a new timeline of the specified type <paramref name="T"/>.
    /// </summary>
    /// <param name="size">The max number of frames the timeline can store.</param>
    public Timeline(int size, T initialFrame)
    {
        _size = size;
        _frames = new T[size];
        _currentFrame = 0;
        _backFrame = 0;
        _frontFrame = 0;
        _frames[0] = initialFrame;
    }

    /// <summary>
    /// Advances the timeline, adding a <paramref name="newFrame"/> after the current
    /// frame.
    /// </summary>
    /// <param name="newFrame">The frame being added. This should be a shallow copy provided
    ///                         by the client.</param>
    public void Advance(T newFrame)
    {
        _currentFrame = WrapIndex(_currentFrame + 1);
        _frames[_currentFrame] = newFrame;
        _frontFrame = _currentFrame;

        if (_frontFrame == _backFrame) {
            _backFrame = WrapIndex(_backFrame + 1);
        }
    }

    public T UndoUntilLock() {
        if (!CanUndo()) return _frames[_currentFrame];

        _currentFrame = _backFrame;
        return _frames[_currentFrame];
    }

    /// <summary>
    /// Moves the timeline backwards at most <paramref name="n"/> frames. If the
    /// timeline cannot be moved that many frames, it moves until its backmost frame.
    /// </summary>
    /// <param name="n">The number of frames to move back.</param>
    /// <returns>
    /// The frame <paramref name="n"/> frames behind the most recent frame,
    /// or the backmost frame if out of bounds.
    /// </returns>
    public T Undo(int n)
    {
        if (!CanUndo()) return _frames[_currentFrame];

        int newIndex = _currentFrame;
        for (int i = 0; i < n; i++) {
            newIndex = WrapIndex(newIndex - 1);
            if (newIndex == _backFrame) {
                break;
            }
        }
        _currentFrame = newIndex;
        return _frames[newIndex];
    }

    /// <summary>
    /// Moves the timeline forwards at most <paramref name="n"/> frames. If the
    /// timeline cannot be moved that many frames, it moves until its frontmost frame.
    /// </summary>
    /// <param name="n">The number of frames to move forward.</param>
    /// <returns>
    /// The frame <paramref name="n"/> frames ahead of the most recent frame,
    /// or the frontmost frame if out of bounds.
    /// </returns>
    public T Redo(int n)
    {
        if (!CanRedo()) return _frames[_currentFrame];

        int newIndex = _currentFrame;
        for (int i = 0; i < n; i++) {
            newIndex = WrapIndex(newIndex + 1);
            if (newIndex == _frontFrame) {
                break;
            }
        }
        _currentFrame = newIndex;
        return _frames[newIndex];
    }

    /// <summary>
    /// Returns true if the timeline can be undone, false otherwise.
    /// </summary>
    public bool CanUndo()
    {
        return _currentFrame != _backFrame;
    }

    /// <summary>
    /// Returns true if the timeline can be redone, false otherwise.
    /// </summary>
    public bool CanRedo()
    {
        return _currentFrame != _frontFrame;
    }

    /// <summary>
    /// Updates the timeline such that it cannot be undone past the current frame.
    /// </summary>
    public void Lock()
    {
        _backFrame = _currentFrame;
    }

    /// <summary>
    /// Returns the current frame data.
    /// </summary>
    /// <returns></returns>
    public T GetCurrentFrame() {
        return _frames[_currentFrame];
    }

    /// <summary>
    /// Converts the given index into an equivalent in-bounds index, wrapping if
    /// necessary.
    /// </summary>
    /// <param name="rawIndex">The unprocessed index.</param>
    /// <returns>The rectified index.</returns>
    private int WrapIndex(int rawIndex) {
        while (rawIndex < 0) {
            rawIndex += _size;
        }

        return rawIndex % _size;
    }

    private void DebugPrintTimeline(string message) {
        string output = "";
        for (int i = 0; i < _size; i++) {
            if (i == _currentFrame && i == _frontFrame) {
                output += "| ";
            } else if (i == _currentFrame && i == _backFrame) {
                output += "| ";
            } else if (i == _currentFrame) {
                output += "V ";
            } else if (i == _backFrame) {
                output += "> ";
            } else if (i == _frontFrame) {
                output += "< ";
            } else {
                output += "_ ";
            }
        }
        Debug.Log(message + ": " + output);
    }
}