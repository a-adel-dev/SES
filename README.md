# School Epidemic Simulator(SES)
School Epidemic Simulator is an ABMS that simulates epidemic propagation in school environment. The software is customizable and can be tuned to represent airborne viruses. The current version of the software is tuned towards SARS-CoV-2, The virus that causes COVID-19.
The software enable the user to simulate health protective measures taken by the school, and see their effects in real time.

The aim of the simulation is to determine the effectiveness of various health measures that the school can apply to mitigate the pathogen propagation.

The model is roughly built upon the work of [(Bazant, M.Z., Bush, J.W.M., 2021)](https://www.medrxiv.org/content/10.1101/2020.08.26.20182824v4)

To simulate the school's actions, the user can control the follwoing settings:
  ### 1. Time scale:
Represents how many seconds in simulation time represent a minute in realtime. this number rangens from 1 (*One second in simulation time represents one minute in realtime*) to 60 (*simulation and realtime are the same*). 
  ### 2. Simulation runtime:
How long in days the simulation will run.
  ### 3. Number of period per schoolday:
Represents how many periods are in a school day.
  ### 4. Period and break length in minutes:
Controls the duration of period and break length in minutes, respectively.
  ### 5. Enable classroom activities:
Normally, students in classroom participate in various activities that invites them to change their positions, and move towards other students, this of course can increase the chance of infection. The oprion can enable or disable these activities.
  ### 6. Enable classroom relocation:
Normally, classrooms relocate to special classes like laboratories and/or arts classes. This option enable/disable the behavior.
  ### 7. Initial number of infected agents:
Controls the initial number of infected students and/or teachers.
  ### 8. Egress waiting time in minutes:
How long should classes wait before the other class have want out of their room in home time.
  ### 9. Mask settings
Controls the types of masks students/teachers are wearing.
  ### 10. Global air control:
Controls the setup of air in classrooms.
  ### 11. School runs at half capacity:
change the number of working classrooms to half.
  ### 12. Classroom running at half capacity
Change the number of students ion class to half.

At the end of the runtime of the simulation, the user can determine the effectiveness of one or more measures by finding how many new infections have occures through out the runtime of the simulation.
