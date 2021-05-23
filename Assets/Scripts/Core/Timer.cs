using UnityEngine;

namespace SES.Core
{
    /// <summary>
    /// A timer
    /// </summary>
    public class Timer : MonoBehaviour
    {
        #region Fields

        // timer duration
        [SerializeField] float duration = 0;

        // timer execution
        [SerializeField]
        float elapsedSeconds = 0;
        [SerializeField]
        bool running = false;

        // support for Finished property
        [SerializeField]
        bool started = false;

        #endregion

        private void Start()
        {
            Run();
        }


        #region Properties

        public float Duration
        {
            set 
            {
                if (running == false)
                {
                    //duration = value;
                    duration = value * SimulationParameters.timeStep;
                    Debug.Log(duration);
                }
            }
        }
        /// <summary>
        /// Gets whether or not the timer has finished running
        /// This property returns false if the timer has never been started
        /// </summary>
        /// <value>true if finished; otherwise, false.</value>
        public bool Finished
        {
            get { return started && !running; }
        }

        /// <summary>
        /// Gets whether or not the timer is currently running
        /// </summary>
        /// <value>true if running; otherwise, false.</value>
        public bool Running
        {
            get { return running; }
        }

        /// <summary>
        /// Gets ho wmany seconds are left on the timer
        /// </summary>
        /// <value>seconds left</value>
        public float SecondsLeft
        {
            get
            {
                if (running)
                {
                    return duration - elapsedSeconds;
                }
                else
                {
                    return 0;
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {
            // update timer and check for finished
            if (running)
            {
                Debug.Log($"Timer is running");
                elapsedSeconds += Time.deltaTime;
                if (elapsedSeconds >= duration)
                {
                    running = false;
                    Debug.Log("Timer is done");
                }
            }
        }

        /// <summary>
        /// Runs the timer
        /// Because a timer of 0 duration doesn't really make sense,
        /// the timer only runs if the total seconds is larger than 0
        /// This also makes sure the consumer of the class has actually 
        /// set the duration to something higher than 0
        /// </summary>
        public void Run()
        {
            // only run with valid duration
            if (duration > 0)
            {
                started = true;
                running = true;
                elapsedSeconds = 0;
            }
        }

        /// <summary>
        /// Stops the timer
        /// </summary>
        public void Stop()
        {
            started = false;
            running = false;
        }

        /// <summary>
        /// Adds the given number of seconds to the timer
        /// </summary>
        /// <param name="seconds">time to add</param>
        public void AddTime(float seconds)
        {
            duration += seconds;
        }

        #endregion
    }

}
