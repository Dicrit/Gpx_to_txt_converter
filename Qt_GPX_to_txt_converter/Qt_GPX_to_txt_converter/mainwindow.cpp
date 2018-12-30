#include "mainwindow.h"
#include "ui_mainwindow.h"
#include <qfiledialog.h>
#include "gpxconverter.h"
#include <QMessageBox>


MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    bool connected = connect(ui->Button_path, SIGNAL(released()), this, SLOT(SelectPath()));
	if (!connected)
	{
        QMessageBox m;
        m.setText("error while binding button");
        m.exec();
	}
}


MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::SelectPath()
{
	QString input = QFileDialog::getOpenFileName(this, tr("Select gpx file"), "",
		tr("Gpx files (*.gpx);;All Files (*)"));
	if (input.isEmpty())
	{
		return;
	}
	QFileInfo info(input);
	QString output = info.path() +tr("\\")+ info.baseName() + tr(".txt");
	output.replace("\\\\", "\\");
	GPXConverter converter;
    connect(&converter, SIGNAL(Progress(int)), ui->progressBar, SLOT(setValue(int)));

	converter.ConvertGpx(input, output);
}
