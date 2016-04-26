ParticipantNumber = 21#Number of particpants in the study.
DataSetsPerPartcipant = 4#Number of datasets in the .csv file for each participant.
ColumnToPlot <- 1#Specify the first column of the dataset we will look at firt (The first one, duh..)

for(i in 1:9)
{
  if(i == 1)
  {
    ImportedData <- read.csv("C:/Users/Jack/OneDrive/MSc/My Studies/Head Movement Study/Results Data/Time Normalised .csv files/NVR Task 1 Time Normalised.csv", header = TRUE)
    dir.create("C:/Users/Jack/Desktop/NVR Task 1")
    dir.create("C:/Users/Jack/Desktop/NVR Task 1/Time Series")
    dir.create("C:/Users/Jack/Desktop/NVR Task 1/Difference Plots")
    dir.create("C:/Users/Jack/Desktop/NVR Task 1/Acceleration Plots")
    
    BaseFolder <- "C:/Users/Jack/Desktop/NVR Task 1"
    TimeSeriesFolder <- "C:/Users/Jack/Desktop/NVR Task 1/Time Series"
    DifferencePlotFolder <- "C:/Users/Jack/Desktop/NVR Task 1/Difference Plots"
    AccelerationPlotFolder <- "C:/Users/Jack/Desktop/NVR Task 1/Acceleration Plots"
  }
  if(i == 2)
  {
    ImportedData <- read.csv("C:/Users/Jack/OneDrive/MSc/My Studies/Head Movement Study/Results Data/Time Normalised .csv files/NVR Task 2 Time Normalised.csv", header = TRUE)
    dir.create("C:/Users/Jack/Desktop/NVR Task 2")
    dir.create("C:/Users/Jack/Desktop/NVR Task 2/Time Series")
    dir.create("C:/Users/Jack/Desktop/NVR Task 2/Difference Plots")
    dir.create("C:/Users/Jack/Desktop/NVR Task 2/Acceleration Plots")
    
    BaseFolder <- "C:/Users/Jack/Desktop/NVR Task 2"
    TimeSeriesFolder <- "C:/Users/Jack/Desktop/NVR Task 2/Time Series"
    DifferencePlotFolder <- "C:/Users/Jack/Desktop/NVR Task 2/Difference Plots"
    AccelerationPlotFolder <- "C:/Users/Jack/Desktop/NVR Task 2/Acceleration Plots"
  }
  if(i == 3)
  {
    ImportedData <- read.csv("C:/Users/Jack/OneDrive/MSc/My Studies/Head Movement Study/Results Data/Time Normalised .csv files/NVR Task 3 Time Normalised.csv", header = TRUE)
    dir.create("C:/Users/Jack/Desktop/NVR Task 3")
    dir.create("C:/Users/Jack/Desktop/NVR Task 3/Time Series")
    dir.create("C:/Users/Jack/Desktop/NVR Task 3/Difference Plots")
    dir.create("C:/Users/Jack/Desktop/NVR Task 3/Acceleration Plots")
    
    BaseFolder <- "C:/Users/Jack/Desktop/NVR Task 3"
    TimeSeriesFolder <- "C:/Users/Jack/Desktop/NVR Task 3/Time Series"
    DifferencePlotFolder <- "C:/Users/Jack/Desktop/NVR Task 3/Difference Plots"
    AccelerationPlotFolder <- "C:/Users/Jack/Desktop/NVR Task 3/Acceleration Plots"
  }
  if(i == 4)
  {
    ImportedData <- read.csv("C:/Users/Jack/OneDrive/MSc/My Studies/Head Movement Study/Results Data/Time Normalised .csv files/VR Task 1 Time Normalised.csv", header = TRUE)
    dir.create("C:/Users/Jack/Desktop/VR Task 1")
    dir.create("C:/Users/Jack/Desktop/VR Task 1/Time Series")
    dir.create("C:/Users/Jack/Desktop/VR Task 1/Difference Plots")
    dir.create("C:/Users/Jack/Desktop/VR Task 1/Acceleration Plots")
    
    BaseFolder <- "C:/Users/Jack/Desktop/VR Task 1"
    TimeSeriesFolder <- "C:/Users/Jack/Desktop/VR Task 1/Time Series"
    DifferencePlotFolder <- "C:/Users/Jack/Desktop/VR Task 1/Difference Plots"
    AccelerationPlotFolder <- "C:/Users/Jack/Desktop/VR Task 1/Acceleration Plots"
  }
  if(i == 5)
  {
    ImportedData <- read.csv("C:/Users/Jack/OneDrive/MSc/My Studies/Head Movement Study/Results Data/Time Normalised .csv files/VR Task 2 Time Normalised.csv", header = TRUE)
    dir.create("C:/Users/Jack/Desktop/VR Task 2")
    dir.create("C:/Users/Jack/Desktop/VR Task 2/Time Series")
    dir.create("C:/Users/Jack/Desktop/VR Task 2/Difference Plots")
    dir.create("C:/Users/Jack/Desktop/VR Task 2/Acceleration Plots")
    
    BaseFolder <- "C:/Users/Jack/Desktop/VR Task 2"
    TimeSeriesFolder <- "C:/Users/Jack/Desktop/VR Task 2/Time Series"
    DifferencePlotFolder <- "C:/Users/Jack/Desktop/VR Task 2/Difference Plots"
    AccelerationPlotFolder <- "C:/Users/Jack/Desktop/VR Task 2/Acceleration Plots"
  }
  if(i == 6)
  {
    ImportedData <- read.csv("C:/Users/Jack/OneDrive/MSc/My Studies/Head Movement Study/Results Data/Time Normalised .csv files/VR Task 3 Time Normalised.csv", header = TRUE)
    dir.create("C:/Users/Jack/Desktop/VR Task 3")
    dir.create("C:/Users/Jack/Desktop/VR Task 3/Time Series")
    dir.create("C:/Users/Jack/Desktop/VR Task 3/Difference Plots")
    dir.create("C:/Users/Jack/Desktop/VR Task 3/Acceleration Plots")
    
    BaseFolder <- "C:/Users/Jack/Desktop/VR Task 3"
    TimeSeriesFolder <- "C:/Users/Jack/Desktop/VR Task 3/Time Series"
    DifferencePlotFolder <- "C:/Users/Jack/Desktop/VR Task 3/Difference Plots"
    AccelerationPlotFolder <- "C:/Users/Jack/Desktop/VR Task 3/Acceleration Plots"
  }
  if(i == 7)
  {
    ImportedData <- read.csv("C:/Users/Jack/OneDrive/MSc/My Studies/Head Movement Study/Results Data/Time Normalised .csv files/NVRW Task 1 Time Normalised.csv", header = TRUE)
    dir.create("C:/Users/Jack/Desktop/NVRW Task 1")
    dir.create("C:/Users/Jack/Desktop/NVRW Task 1/Time Series")
    dir.create("C:/Users/Jack/Desktop/NVRW Task 1/Difference Plots")
    dir.create("C:/Users/Jack/Desktop/NVRW Task 1/Acceleration Plots")
    
    BaseFolder <- "C:/Users/Jack/Desktop/NVRW Task 1"
    TimeSeriesFolder <- "C:/Users/Jack/Desktop/NVRW Task 1/Time Series"
    DifferencePlotFolder <- "C:/Users/Jack/Desktop/NVRW Task 1/Difference Plots"
    AccelerationPlotFolder <- "C:/Users/Jack/Desktop/NVRW Task 1/Acceleration Plots"
  }
  if(i == 8)
  {
    ImportedData <- read.csv("C:/Users/Jack/OneDrive/MSc/My Studies/Head Movement Study/Results Data/Time Normalised .csv files/NVRW Task 2 Time Normalised.csv", header = TRUE)
    dir.create("C:/Users/Jack/Desktop/NVRW Task 2")
    dir.create("C:/Users/Jack/Desktop/NVRW Task 2/Time Series")
    dir.create("C:/Users/Jack/Desktop/NVRW Task 2/Difference Plots")
    dir.create("C:/Users/Jack/Desktop/NVRW Task 2/Acceleration Plots")
    
    BaseFolder <- "C:/Users/Jack/Desktop/NVRW Task 2"
    TimeSeriesFolder <- "C:/Users/Jack/Desktop/NVRW Task 2/Time Series"
    DifferencePlotFolder <- "C:/Users/Jack/Desktop/NVRW Task 2/Difference Plots"
    AccelerationPlotFolder <- "C:/Users/Jack/Desktop/NVRW Task 2/Acceleration Plots"
  }
  if(i == 9)
  {
    ImportedData <- read.csv("C:/Users/Jack/OneDrive/MSc/My Studies/Head Movement Study/Results Data/Time Normalised .csv files/NVRW Task 3 Time Normalised.csv", header = TRUE)
    dir.create("C:/Users/Jack/Desktop/NVRW Task 3")
    dir.create("C:/Users/Jack/Desktop/NVRW Task 3/Time Series")
    dir.create("C:/Users/Jack/Desktop/NVRW Task 3/Difference Plots")
    dir.create("C:/Users/Jack/Desktop/NVRW Task 3/Acceleration Plots")
    
    BaseFolder <- "C:/Users/Jack/Desktop/NVRW Task 3"
    TimeSeriesFolder <- "C:/Users/Jack/Desktop/NVRW Task 3/Time Series"
    DifferencePlotFolder <- "C:/Users/Jack/Desktop/NVRW Task 3/Difference Plots"
    AccelerationPlotFolder <- "C:/Users/Jack/Desktop/NVRW Task 3/Acceleration Plots"
  }
  
  #Generates Time Series Plots.
  for(i in 1:ParticipantNumber)
  {
    for(j in 1:DataSetsPerPartcipant)
    {
      if(j == 1)
      {
        RecordedData = "Heading"
      }
      if(j == 2)
      {
        RecordedData = "Roll"
      }
      if(j ==3)
      {
        RecordedData = "Pitch"
      }
      if(j == 4)
      {
        RecordedData = "Time"
      }
      
      mypath <- file.path(TimeSeriesFolder, paste("Participant ", i,"(",RecordedData,")",".png", sep = ""))#Sets the folder that we will write into
      png(file = mypath)#Sets the file type for the file which is about to be created.
      plot.ts(ImportedData[ColumnToPlot])#Plots the time sereis for the selected data.
      dev.off()#Closes the opened file.
      ColumnToPlot <- ColumnToPlot + 1#Increments the colum we will plot every loop. First loop = 1,2,3,4. Second loop = 5,6,7,8 ext.
    }
  }
  ColumnToPlot <- 1#When this loop has finished, we need to reset the column plot so that it can be resued by the next loop.
  
  #Generates Difference Plots.
  for(i in 1:ParticipantNumber)
  {
    for(j in 1:DataSetsPerPartcipant)
    {
      if(j == 1)
      {
        RecordedData = "Heading"
      }
      if(j == 2)
      {
        RecordedData = "Roll"
      }
      if(j ==3)
      {
        RecordedData = "Pitch"
      }
      if(j == 4)
      {
        RecordedData = "Time"
      }
      
      mypath <- file.path(DifferencePlotFolder, paste("Participant ", i,"(",RecordedData,")",".png", sep = ""))
      png(file = mypath)
      plot.ts(diff(ImportedData[,c(ColumnToPlot)]))#Plots the difference between data recordings.
      dev.off()
      ColumnToPlot <- ColumnToPlot + 1
    }
  }
  ColumnToPlot <- 1
  
  #Generate Acceleration (Difference of Difference Plots)
  for(i in 1:ParticipantNumber)
  {
    for(j in 1:DataSetsPerPartcipant)
    {
      if(j == 1)
      {
        RecordedData = "Heading"
      }
      if(j == 2)
      {
        RecordedData = "Roll"
      }
      if(j ==3)
      {
        RecordedData = "Pitch"
      }
      if(j == 4)
      {
        RecordedData = "Time"
      }
      
      mypath <- file.path(AccelerationPlotFolder, paste("Participant ", i,"(",RecordedData,")",".png", sep = ""))
      png(file = mypath)
      plot.ts(diff(diff(ImportedData[,c(ColumnToPlot)])))#Plots the difference of the difference between data recordings.
      dev.off()
      ColumnToPlot <- ColumnToPlot + 1
    }
  }
  ColumnToPlot <- 1
  
  
  #NULL all folder paths, just to be sure.
  ImportedData <- NULL
  BaseFolder <- NULL
  TimeSeriesFolder <- NULL
  DifferencePlotFolder <- NULL
  AccelerationPlotFolder <-NULL
}
print("Completed")